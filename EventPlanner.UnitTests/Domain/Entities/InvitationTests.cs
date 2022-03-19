using System;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using EventPlanner.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Domain.Entities;

[TestFixture]
public class InvitationTests
{
    [Test]
    public void AcceptsTheInvitation()
    {
        var invitation = new Invitation(Guid.Empty, InvitationStatus.Pending, "john@gmail.com");

        invitation.Accept();

        invitation.Status.Should().Be(InvitationStatus.Accepted);
    }

    [Test]
    public void DeclinesTheInvitation()
    {
        var invitation = new Invitation(Guid.Empty, InvitationStatus.Pending, "john@gmail.com");

        invitation.Decline();

        invitation.Status.Should().Be(InvitationStatus.Declined);
    }

    [Test]
    public void ThrowsAnExceptionWhenTryingToVoteTheSameThing()
    {
        var invitation = new Invitation(Guid.Empty, InvitationStatus.Accepted, "john@gmail.com");
        var actAccept = () => invitation.Accept();

        var invitationTwo = new Invitation(Guid.Empty, InvitationStatus.Declined, "john@gmail.com");
        var actDecline = () => invitationTwo.Decline();

        actAccept.Should().Throw<CannotCastTheSameVoteException>();
        actDecline.Should().Throw<CannotCastTheSameVoteException>();
    }
}