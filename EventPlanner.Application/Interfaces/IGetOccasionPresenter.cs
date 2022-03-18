using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface IGetOccasionPresenter<Output> : IPresenter<OccasionWithInvitationsDTO, Output>
{
}