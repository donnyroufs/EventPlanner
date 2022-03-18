using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionsByRangeUseCase<Output> : IUseCase<GetOccasionsByRangeDTO, Output>
{
    private readonly IGetOccasionsByRangePresenter<Output> _presenter;
    private readonly IOccasionRepository _repository;

    public GetOccasionsByRangeUseCase(IGetOccasionsByRangePresenter<Output> presenter, IOccasionRepository repository)
    {
        _presenter = presenter;
        _repository = repository;
    }

    public async Task<Output> Execute(GetOccasionsByRangeDTO data)
    {
        var occasions = await _repository.FindByRange(data.OccassionId, data.Range);

        return _presenter.Present(occasions.Select(OccasionDTO.From).ToList());
    }
}