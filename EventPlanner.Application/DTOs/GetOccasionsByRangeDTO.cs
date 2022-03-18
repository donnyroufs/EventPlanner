using System;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Application.DTOs;

public class GetOccasionsByRangeDTO
{
    public DateRange Range { get; init; }

    public GetOccasionsByRangeDTO(DateRange range)
    {
        Range = range;
    }
}