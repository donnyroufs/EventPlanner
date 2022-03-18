using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface ICreateOccasionPresenter<Output> : IPresenter<OccasionDTO, Output>
{
}