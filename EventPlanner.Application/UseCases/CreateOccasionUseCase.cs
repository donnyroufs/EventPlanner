using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain;
using EventPlanner.Domain.Entities;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class CreateOccasionUseCase : IUseCase<CreateOccasionDTO>
{
    private readonly ICreateOccasionPresenter _presenter;
    private readonly IOccasionRepository _occasionRepository;

    public CreateOccasionUseCase(IOccasionRepository occasionRepository, ICreateOccasionPresenter presenter)
    {
        _occasionRepository = occasionRepository;
        _presenter = presenter;
    }

    public async Task Execute(CreateOccasionDTO data)
    {
        var occasion = new Occasion(data.Description, data.Days);

        var createdOccasion = await _occasionRepository.Save(occasion);

        await _presenter.Present(OccasionDTO.From(createdOccasion));
    }
}