using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface IReplyToInvitationPresenter<Output> : IPresenter<InvitationDTO, Output>
{
}