using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionUseCase<Output> : IUseCase<GetOccasionDTO, Output>
{
    private readonly IGetOccasionPresenter<Output> _presenter;
    private readonly IOccasionRepository _occasionRepository;

    public GetOccasionUseCase(IGetOccasionPresenter<Output> presenter, IOccasionRepository occasionRepository)
    {
        _presenter = presenter;
        _occasionRepository = occasionRepository;
    }

    public async Task<Output> Execute(GetOccasionDTO data)
    {
        var occasion = await _occasionRepository.Find(data.Id);

        if (occasion is null)
        {
            throw new OccasionDoesNotExistException();
        }

        return _presenter.Present(
            new OccasionWithInvitationsDTO(OccasionDTO.From(occasion),
                occasion.Invitations.Select(InvitationDTO.From).ToList())
        );
    }
}