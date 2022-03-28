using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Application.UseCases;

[TestFixture]
public class GetOccasionUseCaseTests
{
    private Presenter _presenter = null!;
    private Mock<IOccasionRepository> _occasionRepository = null!;
    private GetOccasionUseCase<GetOccasionViewModel> _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new Presenter();
        _occasionRepository = new Mock<IOccasionRepository>();

        _sut =
            new GetOccasionUseCase<GetOccasionViewModel>(_presenter, _occasionRepository.Object);
    }

    [Test]
    public async Task ReturnsTheRequestOccasionWithInvitations()
    {
        var occasionId = Guid.NewGuid();
        var f = new Fixture();
        var invitation = f.Create<Invitation>();

        var occasion = f.Build<Occasion>().Do(o => o.AddInvitation(invitation)).Create();

        _occasionRepository.Setup(x => x.Find(occasionId)).ReturnsAsync(occasion);

        var dto = new GetOccasionDTO(occasionId);

        var result = await _sut.Execute(dto);

        result.Should().BeOfType<GetOccasionViewModel>();
    }

    [Test]
    public async Task ShouldThrowWhenOccasionDoesNotExist()
    {
        var occasionId = Guid.NewGuid();

        var dto = new GetOccasionDTO(occasionId);

        var act = () => _sut.Execute(dto);

        await act.Should().ThrowAsync<OccasionDoesNotExistException>();
    }

    private class GetOccasionViewModel
    {
    }

    private class Presenter : IGetOccasionPresenter<GetOccasionViewModel>
    {
        public GetOccasionViewModel Present(OccasionWithInvitationsDTO data)
        {
            return new GetOccasionViewModel();
        }
    }
}