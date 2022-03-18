using Microsoft.AspNetCore.Mvc;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.UseCases;
using EventPlanner.WebAPI.Requests;
using EventPlanner.WebAPI.Responses;

namespace EventPlanner.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OccasionsController : ControllerBase
{
    private readonly GetOccasionsByRangeUseCase<OccasionsResponse> _getOccasionsByRangeUseCase;
    private readonly CreateOccasionUseCase<OccasionResponse> _createOccasionUseCase;
    private readonly GetOccasionUseCase<OccasionWithInvitationsResponse> _getOccasionUseCase;

    public OccasionsController(GetOccasionsByRangeUseCase<OccasionsResponse> getOccasionsByRangeUseCase,
        CreateOccasionUseCase<OccasionResponse> createOccasionUseCase,
        GetOccasionUseCase<OccasionWithInvitationsResponse> getOccasionUseCase)
    {
        _getOccasionsByRangeUseCase = getOccasionsByRangeUseCase;
        _createOccasionUseCase = createOccasionUseCase;
        _getOccasionUseCase = getOccasionUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] GetOccasionsByRangeRequest data)
    {
        var occasions = await _getOccasionsByRangeUseCase.Execute(data.ToDomain());

        return Ok(occasions);
    }

    [HttpGet("/occasions/{id}")]
    public async Task<IActionResult> GetWithInvitations([FromRoute] Guid id)
    {
        var result = await _getOccasionUseCase.Execute(new GetOccasionDTO(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Store([FromBody] CreateOccasionRequest body)
    {
        var result = await _createOccasionUseCase.Execute(body.ToDomain());

        return Created(nameof(Store), result);
    }
}