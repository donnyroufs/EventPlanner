namespace EventPlanner.WebAPI.ProblemDetails;

public class ValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public ValidationProblemDetails(string message)
    {
        Type = "https://docs.eventplanner.com/validation-error";
        Title = "Validation Error";
        Status = 400;
        Detail = message;
    }
}