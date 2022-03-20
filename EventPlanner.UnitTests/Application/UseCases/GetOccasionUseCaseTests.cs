using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [Test]
    public async Task ReturnsTheRequestOccasionWithInvitations()
    {
        var presenter = new Presenter();
        var occasionRepository = new Mock<IOccasionRepository>();
        var invitationsRepository = new Mock<IInvitationRepository>();
        var occasionId = Guid.NewGuid();

        occasionRepository.Setup(x => x.Find(occasionId))
            .ReturnsAsync(new Occasion("description", new List<DayOfWeek>
            {
                DayOfWeek.Friday
            }));

        invitationsRepository.Setup(x => x.FindByOccasionId(occasionId)).ReturnsAsync(new List<Invitation>());

        var useCase =
            new GetOccasionUseCase<GetOccasionViewModel>(presenter, occasionRepository.Object,
                invitationsRepository.Object);

        var dto = new GetOccasionDTO(occasionId);

        var result = await useCase.Execute(dto);

        result.Should().BeOfType<GetOccasionViewModel>();
    }

    [Test]
    public async Task ShouldThrowWhenOccasionDoesNotExist()
    {
        var presenter = new Presenter();
        var occasionRepository = new Mock<IOccasionRepository>();
        var invitationsRepository = new Mock<IInvitationRepository>();
        var occasionId = Guid.NewGuid();

        var useCase =
            new GetOccasionUseCase<GetOccasionViewModel>(presenter, occasionRepository.Object,
                invitationsRepository.Object);

        var dto = new GetOccasionDTO(occasionId);

        var act = () => useCase.Execute(dto);

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