using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class ReplyToInvitationUseCase<Output> : IUseCase<ReplyToInvitationDTO, Output>
{
    private readonly IReplyToInvitationPresenter<Output> _presenter;
    private readonly IInvitationRepository _invitationRepository;
    private readonly INotify _notifier;

    public ReplyToInvitationUseCase(IReplyToInvitationPresenter<Output> presenter,
        IInvitationRepository invitationRepository, INotify notifier)
    {
        _presenter = presenter;
        _invitationRepository = invitationRepository;
        _notifier = notifier;
    }

    public async Task<Output> Execute(ReplyToInvitationDTO data)
    {
        var invitation = await _invitationRepository.Find(data.InvitationId);

        // TODO: Remove UserEmail no need to pass data
        UpdateInvitationStatus(data, invitation);

        await _invitationRepository.Save(invitation);

        await NotifyAdmin(data);

        return _presenter.Present(InvitationDTO.From(invitation));
    }

    private async Task NotifyAdmin(ReplyToInvitationDTO data)
    {
        var message =
            MessageFactory.CreateRepliedToInvitation(data.UserEmail, data.InvitationId);

        await _notifier.Notify(message);
    }

    private void UpdateInvitationStatus(ReplyToInvitationDTO data, Invitation invitation)
    {
        if (data.Accepted)
        {
            invitation.Accept();
            return;
        }

        invitation.Decline();
    }
}