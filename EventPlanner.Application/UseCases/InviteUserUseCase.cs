using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class InviteUserUseCase<Output> : IUseCase<InviteUserDTO, Output>
{
    private readonly IInviteUserPresenter<Output> _presenter;
    private readonly INotify _notifier;
    private readonly IOccasionRepository _occasionRepository;
    private readonly IInvitationRepository _invitationRepository;

    public InviteUserUseCase(IInviteUserPresenter<Output> presenter, INotify notifier,
        IOccasionRepository occasionRepository, IInvitationRepository invitationRepository)
    {
        _presenter = presenter;
        _notifier = notifier;
        _occasionRepository = occasionRepository;
        _invitationRepository = invitationRepository;
    }

    public async Task<Output> Execute(InviteUserDTO data)
    {
        var occasion = await GetOccasionOrThrow(data);

        // TODO: If an invitation exists then resend

        var invitation = new Invitation(data.OccasionId, InvitationStatus.Pending, data.Receiver);

        await _invitationRepository.Save(invitation);

        await SendInvitation(occasion, data.Receiver);

        return PresentResult(occasion, invitation);
    }


    private async Task<Occasion> GetOccasionOrThrow(InviteUserDTO data)
    {
        var occasion = await _occasionRepository.Find(data.OccasionId);

        if (occasion == null)
        {
            throw new OccasionDoesNotExistException();
        }

        return occasion;
    }

    private async Task SendInvitation(Occasion occasion, string receiver)
    {
        var message = MessageFactory.CreateInvitation(receiver, occasion.Description);
        await _notifier.Notify(message);
    }

    private Output PresentResult(Occasion occasion, Invitation invitation)
    {
        var dto = OccasionWithInvitationDTO.From(InvitationDTO.From(invitation), OccasionDTO.From(occasion));

        return _presenter.Present(dto);
    }
}