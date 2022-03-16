using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class ReplyToInvitationUseCase<Output> : IUseCase<ReplyToInvitationDTO, Output>
{
    private readonly IPresenter<InvitationDTO, Output> _presenter;
    private readonly IInvitationRepository _invitationRepository;

    public ReplyToInvitationUseCase(IPresenter<InvitationDTO, Output> presenter,
        IInvitationRepository invitationRepository)
    {
        _presenter = presenter;
        _invitationRepository = invitationRepository;
    }

    public async Task<Output> Execute(ReplyToInvitationDTO data)
    {
        var invitation = await _invitationRepository.Find(data.InvitationId);

        updateInvitationStatus(data, invitation);

        await _invitationRepository.Save(invitation);

        return _presenter.Present(InvitationDTO.From(invitation));
    }

    private void updateInvitationStatus(ReplyToInvitationDTO data, Invitation invitation)
    {
        if (data.Accepted)
        {
            invitation.Accept();
            return;
        }

        invitation.Decline();
    }
}