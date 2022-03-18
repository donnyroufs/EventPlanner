using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.WebAPI.Requests;

public class GetOccasionRequest
{
    [FromRoute] public Guid Id { get; set; }
}