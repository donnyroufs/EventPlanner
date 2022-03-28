using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionUseCase : IUseCase<GetOccasionDTO>
{
    private readonly IGetOccasionPresenter _presenter;
    private readonly IOccasionRepository _occasionRepository;

    public GetOccasionUseCase(IGetOccasionPresenter presenter, IOccasionRepository occasionRepository)
    {
        _presenter = presenter;
        _occasionRepository = occasionRepository;
    }

    public async Task Execute(GetOccasionDTO data)
    {
        var occasion = await _occasionRepository.Find(data.Id);

        if (occasion is null)
        {
            throw new OccasionDoesNotExistException();
        }

        await _presenter.Present(
            new OccasionWithInvitationsDTO(OccasionDTO.From(occasion),
                occasion.Invitations.Select(InvitationDTO.From).ToList())
        );
    }
}