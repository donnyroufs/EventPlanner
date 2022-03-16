using System.Threading.Tasks;
using EventPlanner.Application.DTOs;
using EventPlanner.Application.Exceptions;
using EventPlanner.Application.Interfaces;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Factories;
using EventPlanner.Shared;

namespace EventPlanner.Application.UseCases;

public class InviteUserUseCase<Output> : IUseCase<IInviteUserDTO, Output>
{
    private readonly IPresenter<OccasionDTO, Output> _presenter;
    private readonly INotify _notifier;
    private readonly IOccasionRepository _OccasionRepository;

    public InviteUserUseCase(IPresenter<OccasionDTO, Output> presenter, INotify notifier,
        IOccasionRepository OccasionRepository)
    {
        _presenter = presenter;
        _notifier = notifier;
        _OccasionRepository = OccasionRepository;
    }

    public async Task<Output> Execute(IInviteUserDTO data)
    {
        var Occasion = await getOccasionOrThrow(data);

        await sendInvitation(Occasion, data.Receiver);

        return presentResult(Occasion);
    }


    private async Task<Occasion> getOccasionOrThrow(IInviteUserDTO data)
    {
        var Occasion = await _OccasionRepository.Find(data.OccasionId);

        if (Occasion == null)
        {
            throw new OccasionDoesNotExistException();
        }

        return Occasion;
    }

    private async Task sendInvitation(Occasion Occasion, string receiver)
    {
        var message = MessageFactory.CreateInvitation(receiver, Occasion.Description);
        await _notifier.Notify(message);
    }

    private Output presentResult(Occasion Occasion)
    {
        var dto = OccasionDTO.From(Occasion!);
        return _presenter.Present(dto);
    }
}