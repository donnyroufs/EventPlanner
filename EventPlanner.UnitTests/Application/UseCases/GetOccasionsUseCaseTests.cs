using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Application.UseCases;

[TestFixture]
public class GetOccasionsUseCaseTests
{
    [Test]
    public async Task GetsAllOccasions()
    {
        var presenter = new Presenter();
        var repository = new Mock<IOccasionRepository>();
        var occasions = new List<Occasion>()
        {
            new Occasion("Weekly Dinner", new List<DayOfWeek>()
            {
                DayOfWeek.Friday
            }),
            new Occasion("Running with the team", new List<DayOfWeek>()
            {
                DayOfWeek.Monday,
                DayOfWeek.Thursday,
            }),
        };

        repository
            .Setup(x => x.FindMany())
            .ReturnsAsync(occasions);

        var useCase = new GetOccasionsUseCase<OccasionsViewModel>(presenter, repository.Object);

        var result = await useCase.Execute();
        var occasionViewModels =
            new List<OccasionViewModel>(occasions.Select(x => new OccasionViewModel(x.Description, x.Days)));
        var expectedResult = new OccasionsViewModel(occasionViewModels);

        result.Should().BeEquivalentTo(expectedResult);
    }

    private class Presenter : IGetOccasionsPresenter<OccasionsViewModel>
    {
        public OccasionsViewModel Present(List<OccasionDTO> data)
        {
            var occasions = data.Select(x => new OccasionViewModel(x.Description, x.Days)).ToList();
            return new OccasionsViewModel(occasions);
        }
    }

    private class OccasionViewModel
    {
        public string Description { get; init; }
        public List<DayOfWeek> Days { get; init; }

        public OccasionViewModel(string description, List<DayOfWeek> days)
        {
            Description = description;
            Days = days;
        }
    }

    private class OccasionsViewModel
    {
        public List<OccasionViewModel> Occasions { get; }

        public OccasionsViewModel(List<OccasionViewModel> occasions)
        {
            Occasions = occasions;
        }
    }
}