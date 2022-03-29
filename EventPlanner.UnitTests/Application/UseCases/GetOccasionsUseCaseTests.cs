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
public class GetOccasionsUseCaseTests
{
    private Mock<IGetOccasionsPresenter> _presenter = null!;
    private Mock<IOccasionRepository> _repository = null!;
    private GetOccasionsUseCase _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new();
        _repository = new Mock<IOccasionRepository>();
        _sut = new GetOccasionsUseCase(_presenter.Object, _repository.Object);
    }

    [Test]
    public async Task GetsAllOccasions()
    {
        var f = new Fixture();

        var occasions = new List<Occasion>()
        {
            f
                .Build<Occasion>()
                .With(o => o.Days, new List<DayOfWeek> { DayOfWeek.Friday, DayOfWeek.Monday })
                .Create(),
            f
                .Build<Occasion>()
                .With(o => o.Days, new List<DayOfWeek> { DayOfWeek.Monday })
                .Create()
        };

        _repository
            .Setup(x => x.FindMany())
            .ReturnsAsync(occasions);

        await _sut.Execute();

        _presenter.Verify(p => p.Present(It.IsAny<List<OccasionDTO>>()));
    }
}