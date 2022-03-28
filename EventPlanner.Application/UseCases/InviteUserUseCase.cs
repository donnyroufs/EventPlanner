using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Enums;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class InviteUserUseCase : IUseCase<InviteUserDTO>
{
    private readonly IInviteUserPresenter _presenter;
    private readonly INotify _notifier;
    private readonly IOccasionRepository _occasionRepository;

    public InviteUserUseCase(IInviteUserPresenter presenter, INotify notifier,
        IOccasionRepository occasionRepository)
    {
        _presenter = presenter;
        _notifier = notifier;
        _occasionRepository = occasionRepository;
    }

    public async Task Execute(InviteUserDTO data)
    {
        var occasion = await GetOccasionOrThrow(data);

        occasion.AddInvitation(new Invitation(data.OccasionId, InvitationStatus.Pending, data.Receiver));

        await _occasionRepository.Save(occasion);

        await SendInvitation(occasion, data.Receiver);

        await PresentResult(occasion, occasion.Invitations.Last());
    }


    private async Task<Occasion> GetOccasionOrThrow(InviteUserDTO data)
    {
        var occasion = await _occasionRepository.Find(data.OccasionId);

        if (occasion is null)
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

    private async Task PresentResult(Occasion occasion, Invitation invitation)
    {
        var dto = OccasionWithInvitationDTO.From(InvitationDTO.From(invitation), OccasionDTO.From(occasion));

        await _presenter.Present(dto);
    }
}