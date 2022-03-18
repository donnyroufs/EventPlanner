using System;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using EventPlanner.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class ReplyToInvitationUseCaseTests
{
    [TestCase(true, InvitationStatus.Accepted)]
    [TestCase(false, InvitationStatus.Declined)]
    public async Task UpdatesTheInvitationStatus(bool accepted, InvitationStatus status)
    {
        var presenter = new Presenter();
        var notifier = Mock.Of<INotify>();
        var repository = new Mock<IInvitationRepository>();
        repository
            .Setup(x => x.Find(It.IsAny<Guid>()))!
            .ReturnsAsync(new Invitation(Guid.NewGuid(), InvitationStatus.Pending, "john@gmail.com"));

        var useCase = new ReplyToInvitationUseCase<ReplyToInvitationViewModel>(presenter, repository.Object, notifier);

        var dto = new ReplyToInvitationDTO(Guid.Empty, accepted, "john@gmail.com");
        var result = await useCase.Execute(dto);

        repository.Verify(x => x.Save(It.IsAny<Invitation>()), Times.Once);
        result.Status.Should().Be(status);
    }

    [Test]
    public async Task NotifiesTheSystemThatSomeoneHasRepliedToTheirInvitation()
    {
        var presenter = new Presenter();
        var notifier = new Mock<INotify>();
        var repository = new Mock<IInvitationRepository>();
        repository
            .Setup(x => x.Find(It.IsAny<Guid>()))!
            .ReturnsAsync(new Invitation(Guid.NewGuid(), InvitationStatus.Pending, "john@gmail.com"));

        var useCase =
            new ReplyToInvitationUseCase<ReplyToInvitationViewModel>(presenter, repository.Object, notifier.Object);

        var dto = new ReplyToInvitationDTO(Guid.Empty, true, "john@gmail.com");
        await useCase.Execute(dto);

        notifier.Verify(x => x.Notify(It.IsAny<Message>()), Times.Once);
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