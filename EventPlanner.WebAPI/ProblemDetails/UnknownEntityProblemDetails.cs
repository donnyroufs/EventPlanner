namespace EventPlanner.WebAPI.ProblemDetails;

public class UnknownEntityProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public UnknownEntityProblemDetails(string message)
    {
        Type = "https://docs.eventplanner.com/unknown-entity";
        Title = "Unknown entity";
        Status = 404;
        Detail = message;
    }
}