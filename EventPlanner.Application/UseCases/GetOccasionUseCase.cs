using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Interfaces;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class GetOccasionUseCase<Output> : IUseCase<GetOccasionDTO, Output>
{
    private readonly IGetOccasionPresenter<Output> _presenter;
    private readonly IOccasionRepository _occasionRepository;
    private readonly IInvitationRepository _invitationRepository;

    public GetOccasionUseCase(IGetOccasionPresenter<Output> presenter, IOccasionRepository occasionRepository,
        IInvitationRepository invitationRepository)
    {
        _presenter = presenter;
        _occasionRepository = occasionRepository;
        _invitationRepository = invitationRepository;
    }

    public async Task<Output> Execute(GetOccasionDTO data)
    {
        var occasion = await _occasionRepository.Find(data.Id);
        var invitations = await _invitationRepository.FindByOccasionId(data.Id);

        var dto = new OccasionWithInvitationsDTO(OccasionDTO.From(occasion),
            invitations.Select(InvitationDTO.From).ToList());

        return _presenter.Present(dto);
    }
}