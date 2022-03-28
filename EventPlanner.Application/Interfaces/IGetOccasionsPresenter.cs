using System.Collections.Generic;
using EventPlanner.Application.DTOs;

namespace EventPlanner.Application.Interfaces;

public interface IGetOccasionsPresenter : IPresenter<List<OccasionDTO>>
{
}