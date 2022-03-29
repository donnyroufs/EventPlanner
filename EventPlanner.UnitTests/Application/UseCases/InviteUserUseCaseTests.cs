using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Application.UseCases;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.UnitTests.Application.UseCases;

[TestFixture]
public class InviteUserUseCaseTests
{
    private InviteUserUseCase _sut = null!;
    private Mock<IInviteUserPresenter> _presenter = null!;
    private Mock<INotify> _notifier = null!;
    private Mock<IOccasionRepository> _repository = null!;

    [SetUp]
    public void Setup()
    {
        _presenter = new Mock<IInviteUserPresenter>();
        _notifier = new Mock<INotify>();
        _repository = new Mock<IOccasionRepository>();

        _sut = new InviteUserUseCase(_presenter.Object, _notifier.Object, _repository.Object);
    }

    [Test]
    public async Task ThrowsAnExceptionWhenTheOccasionDoesNotExist()
    {
        _repository
            .Setup(x => x.Find(It.IsAny<Guid>()))!
            .ReturnsAsync((Occasion)null);

        var dto = new InviteUserDTO(Guid.Empty, "john@gmail.com");
        var act = () => _sut.Execute(dto);

        await act.Should().ThrowAsync<OccasionDoesNotExistException>();
    }

    // TODO: Add test for duplicate invitation
    [Test]
    public async Task CreatesTheInvitation()
    {
        var f = new Fixture();
        var occasion = f.Build<Occasion>().Create();

        _repository
            .Setup(x => x.Find(It.Is<Guid>(o => o.Equals(occasion.Id))))
            .ReturnsAsync(occasion);

        var dto = new InviteUserDTO(occasion.Id, "john@gmail.com");

        await _sut.Execute(dto);

        _repository.Verify(x => x.Save(It.Is<Occasion>(o => o.Id == occasion.Id)), Times.Once);
    }

    [Test]
    public async Task SendsAnInvitation()
    {
        var f = new Fixture();
        var occasion = f.Build<Occasion>().Create();

        _repository
            .Setup(x => x.Find(It.Is<Guid>(g => g.Equals(occasion.Id))))
            .ReturnsAsync(occasion);

        var dto = new InviteUserDTO(occasion.Id, "john@gmail.com");

        await _sut.Execute(dto);

        _notifier.Verify(x => x.Notify(It.IsAny<Message>()), Times.Once);
    }
}