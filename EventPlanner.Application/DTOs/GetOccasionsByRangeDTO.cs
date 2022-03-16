using System;
using EventPlanner.Domain.ValueObjects;

namespace EventPlanner.Application.DTOs;

public class GetOccasionsByRangeDTO
{
    public DateRange Range { get; init; }
    public Guid OccassionId { get; init; }

    public GetOccasionsByRangeDTO(DateRange range, Guid id)
    {
        Range = range;
        OccassionId = id;
    }
}