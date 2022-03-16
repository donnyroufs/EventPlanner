using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain;
using EventPlanner.Domain.Entities;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class CreateOccasionUseCase<Output> : IUseCase<ICreateOccasionDTO, Output>
{
    private IPresenter<OccasionDTO, Output> _presenter { get; init; }
    private IOccasionRepository _OccasionRepository { get; }

    public CreateOccasionUseCase(IOccasionRepository OccasionRepository, IPresenter<OccasionDTO, Output> presenter)
    {
        _OccasionRepository = OccasionRepository;
        _presenter = presenter;
    }

    public async Task<Output> Execute(ICreateOccasionDTO data)
    {
        var occasion = new Occasion(data.Description, data.Days);

        var createdOccasion = await _OccasionRepository.Save(occasion);

        return _presenter.Present(OccasionDTO.From(createdOccasion));
    }
}