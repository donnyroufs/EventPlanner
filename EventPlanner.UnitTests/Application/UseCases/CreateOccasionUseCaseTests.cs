using System;
using System.Collections.Generic;
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
public class Tests
{
    private Presenter _presenter = null!;
    private Mock<IOccasionRepository> _occasionRepository = null!;
    private CreateOccasionUseCase<OccasionViewModel> _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new Presenter();
        _occasionRepository = new Mock<IOccasionRepository>();
        _sut = new CreateOccasionUseCase<OccasionViewModel>(_occasionRepository.Object, _presenter);
    }

    [Test]
    public async Task CreatesAnOccasion()
    {
        const string description = "my Occasion";
        var days = new List<DayOfWeek>()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday
        };
        var occasion = new Occasion(description, days);
        var occasionViewModel = new OccasionViewModel(description, days);
        _occasionRepository
            .Setup(x => x.Save(It.IsAny<Occasion>()))
            .Returns(Task.FromResult(occasion));
        var input = new CreateOccasionDTO(description, days);

        var result = await _sut.Execute(input);

        _occasionRepository.Verify(o => o.Save(It.IsAny<Occasion>()), Times.Once);
        result.Should().BeOfType<OccasionViewModel>();
        result.Should().BeEquivalentTo(occasionViewModel);
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