using System;

namespace EventPlanner.Application.DTOs;

public class GetOccasionDTO
{
    public GetOccasionDTO(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}