using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionsUseCase<Output> : IUseCase<Output>
{
    private readonly IGetOccasionsPresenter<Output> _presenter;
    private readonly IOccasionRepository _repository;

    public GetOccasionsUseCase(IGetOccasionsPresenter<Output> presenter, IOccasionRepository repository)
    {
        _presenter = presenter;
        _repository = repository;
    }

    public async Task<Output> Execute()
    {
        var occasions = await _repository.FindMany();

        return _presenter.Present(occasions.Select(OccasionDTO.From).ToList());
    }
}