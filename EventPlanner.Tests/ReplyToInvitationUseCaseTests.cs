using System;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class ReplyToInvitationUseCaseTests
{
    /*
     * The invite use-case needs to insert an invitation,
     * this one will find it, and cast its vote.
     */
    [TestCase(true, InvitationStatus.Accepted)]
    [TestCase(false, InvitationStatus.Declined)]
    public async Task UpdatesTheInvitationStatus(bool accepted, InvitationStatus status)
    {
        var presenter = new Presenter();
        var repository = new Mock<IInvitationRepository>();
        repository
            .Setup(x => x.Find(It.IsAny<Guid>()))!
            .ReturnsAsync(new Invitation(Guid.NewGuid(), Guid.NewGuid(), InvitationStatus.Pending, "john@gmail.com"));

        var useCase = new ReplyToInvitationUseCase<ReplyToInvitationViewModel>(presenter, repository.Object);

        var dto = new ReplyToInvitationDTO(Guid.Empty, accepted, "john@gmail.com");
        var result = await useCase.Execute(dto);

        repository.Verify(x => x.Save(It.IsAny<Invitation>()), Times.Once);
        result.Status.Should().Be(status);
    }

    private class ReplyToInvitationViewModel
    {
        public Guid Id { get; init; }
        public InvitationStatus Status { get; init; }

        public ReplyToInvitationViewModel(Guid id, InvitationStatus status)
        {
            Id = id;
            Status = status;
        }
    }

    private class Presenter : IPresenter<InvitationDTO, ReplyToInvitationViewModel>
    {
        public ReplyToInvitationViewModel Present(InvitationDTO data)
        {
            return new ReplyToInvitationViewModel(data.Id, data.Status);
        }
    }
}