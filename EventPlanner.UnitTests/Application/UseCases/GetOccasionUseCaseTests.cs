using System;
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
    private Mock<IGetOccasionPresenter> _presenter = null!;
    private Mock<IOccasionRepository> _occasionRepository = null!;
    private GetOccasionUseCase _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new();
        _occasionRepository = new Mock<IOccasionRepository>();

        _sut =
            new GetOccasionUseCase(_presenter.Object, _occasionRepository.Object);
    }

    [Test]
    public async Task ReturnsTheRequestOccasionWithInvitations()
    {
        var f = new Fixture();
        var invitation = f.Create<Invitation>();

        var occasion = f.Build<Occasion>().Do(o => o.AddInvitation(invitation)).Create();

        _occasionRepository.Setup(x => x.Find(occasion.Id)).ReturnsAsync(occasion);

        var dto = new GetOccasionDTO(occasion.Id);

        await _sut.Execute(dto);

        _presenter.Verify(
            p => p.Present(
                It.Is<OccasionWithInvitationsDTO>(o => o.Occasion.Id == occasion.Id && o.Invitations.Count > 0)),
            Times.Once);
    }

    [Test]
    public async Task ShouldThrowWhenOccasionDoesNotExist()
    {
        var occasionId = Guid.NewGuid();

        var dto = new GetOccasionDTO(occasionId);

        var act = () => _sut.Execute(dto);

        await act.Should().ThrowAsync<OccasionDoesNotExistException>();
    }
}