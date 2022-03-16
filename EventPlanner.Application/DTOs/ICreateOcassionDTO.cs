using System;
using System.Collections.Generic;

namespace EventPlanner.Application.DTOs;

public interface ICreateOcassionDTO
{
    string Description { get; }
    List<DayOfWeek> Days { get; init; }
}
