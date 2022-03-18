using System;
using System.Collections.Generic;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class OccasionTests
{
    [TestCase("", null)]
    [TestCase("", DayOfWeek.Friday)]
    public void ShouldThrowUponBadInput(string description, DayOfWeek day)
    {
        var days = new List<DayOfWeek>();

        if (day != null)
        {
            days.Add(day);
        }

        var act = () => new Occasion("", days);

        act.Should().Throw<ValidationException>();
    }
}