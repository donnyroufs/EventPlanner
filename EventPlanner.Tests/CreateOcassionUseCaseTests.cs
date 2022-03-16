using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Application;
using EventPlanner.Domain;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public async Task CreatesAnOcassion()
    {
        const string description = "my ocassion";
        var days = new List<DayOfWeek>()
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday
        };

        var presenter = new Presenter();
        var ocassionRepository = new Mock<IOcassionRepository>();

        var ocassion = new Ocassion(description, days);
        var createOcassionUseCase = new CreateOcassionUseCase<OcassionViewModel>(ocassionRepository.Object, presenter);
        var ocassionViewModel = new OcassionViewModel(description, days);

        ocassionRepository
            .Setup(x => x.Save(It.IsAny<Ocassion>()))
            .Returns(Task.FromResult(ocassion));

        ICreateOcassionDTO input = new CreateOcassionDTO(description, days);

        var result = await createOcassionUseCase.Execute(input);

        ocassionRepository.Verify(o => o.Save(It.IsAny<Ocassion>()), Times.Once);
        result.Should().BeOfType<OcassionViewModel>();
        result.Should().BeEquivalentTo(ocassionViewModel);
    }

    [Test]
    public async Task ThrowsAnExceptionWhenNoDaysGiven()
    {
        const string description = "my ocassion";
        var days = new List<DayOfWeek>();

        var presenter = new Presenter();
        var ocassionRepository = new Mock<IOcassionRepository>();

        var createOcassionUseCase = new CreateOcassionUseCase<OcassionViewModel>(ocassionRepository.Object, presenter);

        ICreateOcassionDTO input = new CreateOcassionDTO(description, days);

        var act = () => createOcassionUseCase.Execute(input);

        await act.Should().ThrowAsync<OcassionRequiresAtleastOneDayException>();
    }
}

internal class CreateOcassionDTO : ICreateOcassionDTO
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    public CreateOcassionDTO(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }
}

internal class OcassionViewModel
{
    public string Description { get; init; }
    public List<DayOfWeek> Days { get; init; }

    public OcassionViewModel(string description, List<DayOfWeek> days)
    {
        Description = description;
        Days = days;
    }
}

internal class Presenter : IPresenter<OcassionDTO, OcassionViewModel>
{
    public OcassionViewModel Present(OcassionDTO data)
    {
        return new OcassionViewModel(data.Description, data.Days);
    }
}