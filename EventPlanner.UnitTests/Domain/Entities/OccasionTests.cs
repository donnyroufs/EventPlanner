using System;
using System.Collections.Generic;
using AutoFixture;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Domain.Entities;

[TestFixture]
public class OccasionTests
{
    [TestCase("", null)]
    [TestCase("", DayOfWeek.Friday)]
    public void ShouldThrowUponBadInput(string description, DayOfWeek? day)
    {
        var days = new List<DayOfWeek>();

        if (day is not null)
        {
            days.Add(day.Value);
        }

        var act = () => new Occasion("", days);

        act.Should().Throw<ValidationException>();
    }

    [Test]
    public void ShouldThrowWhenAddingTheSameInvitation()
    {
        var occasion = new Occasion(Guid.NewGuid(), "Some description", new List<DayOfWeek>
        {
            DayOfWeek.Friday
        }, new List<Invitation>());

        var f = new Fixture();
        var invitation = f.Create<Invitation>();

        var inv2 = new Invitation(invitation.Id, invitation.OccasionId, invitation.Status, invitation.UserEmail);

        occasion.AddInvitation(invitation);

        var act = () => occasion.AddInvitation(inv2);

        act.Should().Throw<InvitationAlreadyExists>();
    }
}