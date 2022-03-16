using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain;
using EventPlanner.Domain.Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests;

[TestFixture]
public class InviteUserUseCaseTests
{
    [Test]
    public async Task ThrowsAnExceptionWhenTheOcassionDoesNotExist()
    {
        var presenter = new Presenter();
        var notifier = new Mock<INotify>();
        var repository = new Mock<IOcassionRepository>();

        repository
            .Setup(x => x.Find(It.IsAny<Guid>()))!
            .ReturnsAsync((Ocassion)null);

        var useCase = new InviteUserUseCase<InviteUserViewModel>(presenter, notifier.Object, repository.Object);

        var dto = new InviteUserDTO(Guid.Empty, "john@gmail.com");
        var act = () => useCase.Execute(dto);

        await act.Should().ThrowAsync<OcassionDoesNotExistException>();
    }

    [Test]
    public async Task SendsAnInvitation()
    {
        var presenter = new Presenter();
        var notifier = new Mock<INotify>();
        var repository = new Mock<IOcassionRepository>();
        var ocassion = new Ocassion("My ocassion", new List<DayOfWeek>()
        {
            DayOfWeek.Friday
        });

        repository
            .Setup(x => x.Find(It.IsAny<Guid>()))
            .ReturnsAsync(ocassion);

        var useCase = new InviteUserUseCase<InviteUserViewModel>(presenter, notifier.Object, repository.Object);
        var dto = new InviteUserDTO(ocassion.Id, "john@gmail.com");

        await useCase.Execute(dto);

        notifier.Verify(x => x.Notify(It.IsAny<Message>()), Times.Once);
    }

    private class Presenter : IPresenter<OcassionDTO, InviteUserViewModel>
    {
        public InviteUserViewModel Present(OcassionDTO data)
        {
            return new InviteUserViewModel();
        }
    }

    private class InviteUserViewModel
    {
        public InviteUserViewModel()
        {
        }
    }
}