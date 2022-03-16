using System.Threading.Tasks;
using EventPlanner.Domain;
using EventPlanner.Shared;

namespace EventPlanner.Application;

public class CreateOcassionUseCase<Output> : IUseCase<ICreateOcassionDTO, Output>
{
    private IPresenter<OcassionDTO, Output> _presenter { get; init; }
    private IOcassionRepository _ocassionRepository { get; }

    public CreateOcassionUseCase(IOcassionRepository ocassionRepository, IPresenter<OcassionDTO, Output> presenter)
    {
        _ocassionRepository = ocassionRepository;
        _presenter = presenter;
    }

    public async Task<Output> Execute(ICreateOcassionDTO data)
    {
        var ocassion = new Ocassion(data.Description);

        await _ocassionRepository.Save(ocassion);

        return _presenter.Present(OcassionDTO.Create(ocassion));
    }
}