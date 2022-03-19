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
            OccasionId = occasion.Entity.Id,
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
            InvitationId = invitation.Entity.Id,
            UserEmail = "john@gmail.com"
        });

        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"/occasions/{occasion.Entity.Id}/invitations/reply", data);
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<InvitationResponse>(jsonString);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseObj.Status.Should().Be(InvitationStatus.Accepted.ToString());
    }
}