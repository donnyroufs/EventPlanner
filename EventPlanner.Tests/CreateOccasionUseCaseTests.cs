using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public async Task CreatesAnOccasion()
    {
        const string description = "my Occasion";
        var days = new List<DayOfWeek>()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday
        };

        var presenter = new Presenter();
        var occasionRepository = new Mock<IOccasionRepository>();

        var occasion = new Occasion(description, days);
        var createOccasionUseCase = new CreateOccasionUseCase<OccasionViewModel>(occasionRepository.Object, presenter);
        var occasionViewModel = new OccasionViewModel(description, days);

        occasionRepository
            .Setup(x => x.Save(It.IsAny<Occasion>()))
            .Returns(Task.FromResult(occasion));

        var input = new CreateOccasionDTO(description, days);

        var result = await createOccasionUseCase.Execute(input);

        occasionRepository.Verify(o => o.Save(It.IsAny<Occasion>()), Times.Once);
        result.Should().BeOfType<OccasionViewModel>();
        result.Should().BeEquivalentTo(occasionViewModel);
    }

    [Test]
    public async Task ThrowsAnExceptionWhenNoDaysGiven()
    {
        const string description = "my Occasion";
        var days = new List<DayOfWeek>();

        var presenter = new Presenter();
        var occasionRepository = new Mock<IOccasionRepository>();

        var createOccasionUseCase = new CreateOccasionUseCase<OccasionViewModel>(occasionRepository.Object, presenter);

        var input = new CreateOccasionDTO(description, days);

        var act = () => createOccasionUseCase.Execute(input);

        await act.Should().ThrowAsync<OccasionRequiresAtleastOneDayException>();
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

    private class Presenter : ICreateOccasionPresenter<OccasionViewModel>
    {
        public OccasionViewModel Present(OccasionDTO data)
        {
            return new OccasionViewModel(data.Description, data.Days);
        }
    }
}