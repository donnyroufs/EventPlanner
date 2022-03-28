using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionsUseCase : IUseCase
{
    private readonly IGetOccasionsPresenter _presenter;
    private readonly IOccasionRepository _repository;

    public GetOccasionsUseCase(IGetOccasionsPresenter presenter, IOccasionRepository repository)
    {
        _presenter = presenter;
        _repository = repository;
    }

    public async Task Execute()
    {
        var occasions = await _repository.FindMany();

        await _presenter.Present(occasions.Select(OccasionDTO.From).ToList());
    }
}