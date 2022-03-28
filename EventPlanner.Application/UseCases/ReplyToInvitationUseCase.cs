using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class ReplyToInvitationUseCase<Output> : IUseCase<ReplyToInvitationDTO, Output>
{
    private readonly IReplyToInvitationPresenter<Output> _presenter;
    private readonly IOccasionRepository _occasionRepository;
    private readonly INotify _notifier;

    public ReplyToInvitationUseCase(
        IReplyToInvitationPresenter<Output> presenter,
        IOccasionRepository occasionRepository,
        INotify notifier)
    {
        _presenter = presenter;
        _notifier = notifier;
        _occasionRepository = occasionRepository;
    }

    // TODO: We dont need UserEmail
    public async Task<Output> Execute(ReplyToInvitationDTO data)
    {
        var occasion = await _occasionRepository.FindWhereInvitationId(data.InvitationId);

        if (occasion is null)
        {
            throw new InvitationDoesNotExistException(data.InvitationId);
        }

        UpdateInvitationStatus(data, occasion);

        await _occasionRepository.Save(occasion);

        await NotifyAdmin(data);

        return _presenter.Present(InvitationDTO.From(occasion.Invitations.First()));
    }

    private async Task NotifyAdmin(ReplyToInvitationDTO data)
    {
        var message =
            MessageFactory.CreateRepliedToInvitation(data.UserEmail, data.InvitationId);

        await _notifier.Notify(message);
    }

    private void UpdateInvitationStatus(ReplyToInvitationDTO data, Occasion occasion)
    {
        if (data.Accepted)
        {
            occasion.AcceptInvitation(data.InvitationId);
            return;
        }

        occasion.DeclineInvitation(data.InvitationId);
    }
}