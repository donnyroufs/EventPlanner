using Microsoft.AspNetCore.Mvc;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.UseCases;
using EventPlanner.WebAPI.Requests;

namespace EventPlanner.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OccasionsController : ControllerBase
{
    private readonly GetOccasionsUseCase _getOccasionsUseCase;
    private readonly CreateOccasionUseCase _createOccasionUseCase;
    private readonly GetOccasionUseCase _getOccasionUseCase;

    public OccasionsController(GetOccasionsUseCase getOccasionsUseCase,
        CreateOccasionUseCase createOccasionUseCase,
        GetOccasionUseCase getOccasionUseCase)
    {
        _getOccasionsUseCase = getOccasionsUseCase;
        _createOccasionUseCase = createOccasionUseCase;
        _getOccasionUseCase = getOccasionUseCase;
    }

    [HttpGet]
    public async Task Index()
    {
        await _getOccasionsUseCase.Execute();
    }

    [HttpGet("/occasions/{id}")]
    public async Task GetWithInvitations([FromRoute] Guid id)
    {
        await _getOccasionUseCase.Execute(new GetOccasionDTO(id));
    }

    [HttpPost]
    public async Task Store([FromBody] CreateOccasionRequest body)
    {
        await _createOccasionUseCase.Execute(body.ToDomain());
    }
}