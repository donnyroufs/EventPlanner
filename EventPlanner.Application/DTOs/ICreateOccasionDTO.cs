using System;
using System.Collections.Generic;

namespace EventPlanner.Application.DTOs;

public interface ICreateOccasionDTO
{
    string Description { get; }
    List<DayOfWeek> Days { get; init; }
}
