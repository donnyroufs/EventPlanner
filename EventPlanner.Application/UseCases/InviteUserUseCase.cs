using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class InviteUserUseCase<Output> : IUseCase<InviteUserDTO, Output>
{
    private readonly IPresenter<OccasionDTO, Output> _presenter;
    private readonly INotify _notifier;
    private readonly IOccasionRepository _occasionRepository;

    public InviteUserUseCase(IPresenter<OccasionDTO, Output> presenter, INotify notifier,
        IOccasionRepository occasionRepository)
    {
        _presenter = presenter;
        _notifier = notifier;
        _occasionRepository = occasionRepository;
    }

    public async Task<Output> Execute(InviteUserDTO data)
    {
        var occasion = await getOccasionOrThrow(data);

        await sendInvitation(occasion, data.Receiver);

        return presentResult(occasion);
    }


    private async Task<Occasion> getOccasionOrThrow(InviteUserDTO data)
    {
        var occasion = await _occasionRepository.Find(data.OccasionId);

        if (occasion == null)
        {
            throw new OccasionDoesNotExistException();
        }

        return occasion;
    }

    private async Task sendInvitation(Occasion occasion, string receiver)
    {
        var message = MessageFactory.CreateInvitation(receiver, occasion.Description);
        await _notifier.Notify(message);
    }

    private Output presentResult(Occasion occasion)
    {
        var dto = OccasionDTO.From(occasion!);
        return _presenter.Present(dto);
    }
}