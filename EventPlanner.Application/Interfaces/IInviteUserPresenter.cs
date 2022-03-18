using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface IInviteUserPresenter<Output> : IPresenter<OccasionWithInvitationDTO, Output>
{
}