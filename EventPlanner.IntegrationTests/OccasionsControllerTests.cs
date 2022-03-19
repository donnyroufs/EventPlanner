using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.Domain.Enums;
using EventPlanner.Infrastructure.Persistence;
using EventPlanner.Infrastructure.Persistence.Models;
using EventPlanner.WebAPI.Requests;
using FluentAssertions;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.IntegrationTests;

public class OccasionsControllerTests
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
    public async Task ShouldReturnOccasions()
    {
        _dbContext.Occasions.Add(new OccasionModel
        {
            Id = Guid.NewGuid(),
            Description = "Some description",
            Days = new List<OccasionDaysModel>
            {
                new OccasionDaysModel(DayOfWeek.Friday)
            }
        });

        await _dbContext.SaveChangesAsync();

        var result = await _client.GetAsync("/occasions?start_date=20-03-2004&end_date=21-03-2004");

        var json = result.Content.ReadAsStringAsync().Result;
        var data = JsonConvert.DeserializeObject<OccasionsResponse>(json);

        data.Occasions.Count.Should().Be(1);
    }

    [Test]
    public async Task ShouldReturnAnOccasionWithItsSentInvitations()
    {
        var occasionId = Guid.NewGuid();
        var invitationId = Guid.NewGuid();

        await _dbContext.Occasions.AddAsync(new OccasionModel
        {
            Id = occasionId,
            Description = "Some description",
            Days = new Collection<OccasionDaysModel>
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

        var result = await _client.GetAsync($"/occasions/{occasionId}");

        var json = result.Content.ReadAsStringAsync().Result;
        var data = JsonConvert.DeserializeObject<OccasionWithInvitationsResponse>(json);

        data.Invitations.Should().Contain(x => x.Id == invitationId);
        data.Occasion.Id.Should().Be(occasionId);
    }

    [Test]
    public async Task ShouldCreateAnOccasion()
    {
        var json = JsonConvert.SerializeObject(new CreateOccasionRequest
        {
            Description = "My first occasion",
            Days = new List<DayOfWeek>
            {
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            }
        });

        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/occasions", data);
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var responseObj = JsonConvert.DeserializeObject<OccasionResponse>(jsonString);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        responseObj.Description.Should().Be("My first occasion");
    }
}