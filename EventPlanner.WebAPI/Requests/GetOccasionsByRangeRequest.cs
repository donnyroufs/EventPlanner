using EventPlanner.Application.DTOs;
using EventPlanner.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventPlanner.WebAPI.Requests;

public class GetOccasionsByRangeRequest
{
    [BindRequired]
    [FromQuery(Name = "start_date")]
    public string StartDate { get; set; }

    [BindRequired]
    [FromQuery(Name = "end_date")]
    public string EndDate { get; set; }

    public GetOccasionsByRangeDTO ToDomain()
    {
        return new GetOccasionsByRangeDTO(new DateRange(DateTime.Parse(StartDate), DateTime.Parse(EndDate)));
    }
}