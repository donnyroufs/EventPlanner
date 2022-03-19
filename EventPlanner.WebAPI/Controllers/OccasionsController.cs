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
    private readonly GetOccasionsUseCase<OccasionsResponse> _getOccasionsUseCase;
    private readonly CreateOccasionUseCase<OccasionResponse> _createOccasionUseCase;
    private readonly GetOccasionUseCase<OccasionWithInvitationsResponse> _getOccasionUseCase;

    public OccasionsController(GetOccasionsUseCase<OccasionsResponse> getOccasionsUseCase,
        CreateOccasionUseCase<OccasionResponse> createOccasionUseCase,
        GetOccasionUseCase<OccasionWithInvitationsResponse> getOccasionUseCase)
    {
        _getOccasionsUseCase = getOccasionsUseCase;
        _createOccasionUseCase = createOccasionUseCase;
        _getOccasionUseCase = getOccasionUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var occasions = await _getOccasionsUseCase.Execute();

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