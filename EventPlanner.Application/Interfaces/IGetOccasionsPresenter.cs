using System.Collections.Generic;
using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface IGetOccasionsPresenter<Output> : IPresenter<List<OccasionDTO>, Output>
{
}