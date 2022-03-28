using System;
using System.Threading.Tasks;
using AutoFixture;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using EventPlanner.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Application.UseCases;

[TestFixture]
public class ReplyToInvitationUseCaseTests
{
    private Presenter _presenter = null!;
    private Mock<INotify> _notifier = null!;
    private Mock<IOccasionRepository> _repository = null!;
    private ReplyToInvitationUseCase<ReplyToInvitationViewModel> _sut = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new Presenter();
        _notifier = new Mock<INotify>();
        _repository = new Mock<IOccasionRepository>();

        _sut = new ReplyToInvitationUseCase<ReplyToInvitationViewModel>(_presenter, _repository.Object,
            _notifier.Object);
    }

    [TestCase(true, InvitationStatus.Accepted)]
    [TestCase(false, InvitationStatus.Declined)]
    public async Task UpdatesTheInvitationStatus(bool accepted, InvitationStatus status)
    {
        var f = new Fixture();
        var invitation = f.Build<Invitation>().With(inv => inv.Status, InvitationStatus.Pending).Create();
        var occasion = f.Build<Occasion>().Do(x => x.Invitations.Add(invitation)).Create();
        _repository
            .Setup(x => x.FindWhereInvitationId(It.IsAny<Guid>()))!
            .ReturnsAsync(occasion);
        var dto = new ReplyToInvitationDTO(invitation.Id, accepted, invitation.UserEmail);

        var result = await _sut.Execute(dto);

        _repository.Verify(x => x.Save(It.IsAny<Occasion>()), Times.Once);
        result.Status.Should().Be(status);
    }

    [Test]
    public async Task NotifiesTheSystemThatSomeoneHasRepliedToTheirInvitation()
    {
        var f = new Fixture();
        var invitation = f.Build<Invitation>().With(inv => inv.Status, InvitationStatus.Pending).Create();
        var occasion = f.Build<Occasion>().Do(x => x.Invitations.Add(invitation)).Create();
        _repository
            .Setup(x => x.FindWhereInvitationId(It.IsAny<Guid>()))!
            .ReturnsAsync(occasion);
        var dto = new ReplyToInvitationDTO(invitation.Id, true, invitation.UserEmail);

        await _sut.Execute(dto);

        _notifier.Verify(x => x.Notify(It.IsAny<Message>()), Times.Once);
    }

    [Test]
    public async Task ThrowsWhenTheInvitationDoesNotExist()
    {
        var dto = new ReplyToInvitationDTO(Guid.Empty, true, "john@gmail.com");
        var act = () => _sut.Execute(dto);

        await act.Should().ThrowAsync<InvitationDoesNotExistException>();
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

    private class Presenter : IReplyToInvitationPresenter<ReplyToInvitationViewModel>
    {
        public ReplyToInvitationViewModel Present(InvitationDTO data)
        {
            return new ReplyToInvitationViewModel(data.Id, data.Status);
        }
    }
}