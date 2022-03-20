namespace EventPlanner.WebAPI.ProblemDetails;

public class DomainProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public DomainProblemDetails(string message)
    {
        Type = "https://docs.eventplanner.com/invalid-operation";
        Title = "Invalid operation";
        Status = 422;
        Detail = message;
    }
}