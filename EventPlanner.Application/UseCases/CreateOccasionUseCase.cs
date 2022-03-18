using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain;
using EventPlanner.Domain.Entities;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class CreateOccasionUseCase<Output> : IUseCase<CreateOccasionDTO, Output>
{
    private ICreateOccasionPresenter<Output> _presenter { get; init; }
    private IOccasionRepository _occasionRepository { get; }

    public CreateOccasionUseCase(IOccasionRepository occasionRepository, ICreateOccasionPresenter<Output> presenter)
    {
        _occasionRepository = occasionRepository;
        _presenter = presenter;
    }

    public async Task<Output> Execute(CreateOccasionDTO data)
    {
        var occasion = new Occasion(data.Description, data.Days);

        var createdOccasion = await _occasionRepository.Save(occasion);

        return _presenter.Present(OccasionDTO.From(createdOccasion));
    }
}