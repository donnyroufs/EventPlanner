using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Application.UseCases;

[TestFixture]
public class Tests
{
    private Mock<ICreateOccasionPresenter> _presenter = null!;
    private Mock<IOccasionRepository> _occasionRepository = null!;
    private CreateOccasionUseCase _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new Mock<ICreateOccasionPresenter>();
        _occasionRepository = new Mock<IOccasionRepository>();
        _sut = new CreateOccasionUseCase(_occasionRepository.Object, _presenter.Object);
    }

    [Test]
    public async Task CreatesAnOccasion()
    {
        var f = new Fixture();

        var occasion = f
            .Build<Occasion>()
            .With(o => o.Days, new List<DayOfWeek> { DayOfWeek.Friday, DayOfWeek.Monday })
            .Create();

        _occasionRepository
            .Setup(x => x.Save(It.IsAny<Occasion>()))
            .Returns(Task.FromResult(occasion));

        var input = new CreateOccasionDTO(occasion.Description, occasion.Days);

        await _sut.Execute(input);

        _occasionRepository.Verify(o => o.Save(It.Is<Occasion>(x => x.Description == occasion.Description)),
            Times.Once);
        _presenter.Verify(p => p.Present(It.Is<OccasionDTO>(dto => dto.Id == occasion.Id)), Times.Once);
    }
}