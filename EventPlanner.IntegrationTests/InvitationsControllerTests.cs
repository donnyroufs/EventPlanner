using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.Domain.Enums;
using EventPlanner.Infrastructure.Persistence;
using EventPlanner.Infrastructure.Persistence.Models;
using EventPlanner.WebAPI.Requests;
using EventPlanner.WebAPI.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace EventPlanner.IntegrationTests;

[TestFixture]
public class InvitationsControllerTests
{
    private WebApplicationFactory<Program> _application = null!;
    private AppDbContext _dbContext = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        _dbContext = new AppDbContext();
        _client = _application.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task ShouldCreateAnInvitation()
    {
        var occasion = await _dbContext.Occasions.AddAsync(new OccasionModel
        {
            Id = Guid.NewGuid(),
            Description = "Some description",
            Days = new List<OccasionDaysModel>
            {
                new OccasionDaysModel(DayOfWeek.Friday)
            }
        });

        await _dbContext.SaveChangesAsync();

        var json = JsonConvert.SerializeObject(new InviteUserRequest
        {
            Receiver = "john@gmail.com"
        });

        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/occasions/{occasion.Entity.Id}/invitations", data);
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<InvitationResponse>(jsonString);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseObj.Status.Should().Be(InvitationStatus.Pending.ToString());
    }

    [Test]
    public async Task UpdatesInvitationStatus()
    {
        var occasion = await _dbContext.Occasions.AddAsync(new OccasionModel
        {
            Id = Guid.NewGuid(),
            Description = "Some description",
            Days = new List<OccasionDaysModel>
            {
                new OccasionDaysModel(DayOfWeek.Friday)
            }
        });

        var invitation = await _dbContext.Invitations.AddAsync(new InvitationModel
        {
            Status = InvitationStatus.Pending,
            OccasionId = occasion.Entity.Id,
            UserEmail = "john@gmail.com"
        });

        await _dbContext.SaveChangesAsync();

        var json = JsonConvert.SerializeObject(new ReplyToInvitationRequest
        {
            Accepted = true,
            UserEmail = "john@gmail.com"
        });

        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response =
            await _client.PostAsync($"/occasions/{occasion.Entity.Id}/invitations/{invitation.Entity.Id}/reply", data);
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<InvitationResponse>(jsonString);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseObj.Status.Should().Be(InvitationStatus.Accepted.ToString());
    }

    [Test]
    public async Task ReturnsUnknownEntityProblemDetailsWhenNoInvitationFound()
    {
        var occasionId = Guid.NewGuid();

        await _dbContext.Occasions.AddAsync(new OccasionModel
        {
            Id = occasionId,
            Description = "Some description",
            Days = new List<OccasionDaysModel>
            {
                new OccasionDaysModel(DayOfWeek.Friday)
            }
        });

        await _dbContext.SaveChangesAsync();

        var requestJson = JsonConvert.SerializeObject(new ReplyToInvitationRequest
        {
            Accepted = true,
            UserEmail = "john@gmail.com"
        });

        var data = new StringContent(requestJson, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync($"/occasions/{occasionId}/invitations/{Guid.NewGuid()}/reply", data);

        var json = result.Content.ReadAsStringAsync().Result;

        json.Should().Contain("Unknown entity");
    }

    [Test]
    public async Task ReturnsDomainProblemDetailsWhenTryingToCastTheSameVoteTwice()
    {
        var occasionId = Guid.NewGuid();
        var invitationId = Guid.NewGuid();

        await _dbContext.Occasions.AddAsync(new OccasionModel
        {
            Id = occasionId,
            Description = "Some description",
            Days = new List<OccasionDaysModel>
            {
                new OccasionDaysModel(DayOfWeek.Friday)
            }
        });

        await _dbContext.Invitations.AddAsync(new InvitationModel
        {
            Id = invitationId,
            Status = InvitationStatus.Accepted,
            OccasionId = occasionId,
            UserEmail = "john@gmail.com"
        });

        await _dbContext.SaveChangesAsync();

        var requestJson = JsonConvert.SerializeObject(new ReplyToInvitationRequest
        {
            Accepted = true,
            UserEmail = "john@gmail.com"
        });

        var data = new StringContent(requestJson, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync($"/occasions/{occasionId}/invitations/{invitationId}/reply", data);

        var json = result.Content.ReadAsStringAsync().Result;

        json.Should().Contain("Invalid operation");
    }
}