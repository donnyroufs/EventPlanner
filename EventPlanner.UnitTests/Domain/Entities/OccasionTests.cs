using System;
using System.Collections.Generic;
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
}