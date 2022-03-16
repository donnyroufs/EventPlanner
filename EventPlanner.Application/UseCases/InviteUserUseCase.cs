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
    private readonly IPresenter<OcassionDTO, Output> _presenter;
    private readonly INotify _notifier;
    private readonly IOcassionRepository _ocassionRepository;

    public InviteUserUseCase(IPresenter<OcassionDTO, Output> presenter, INotify notifier,
        IOcassionRepository ocassionRepository)
    {
        _presenter = presenter;
        _notifier = notifier;
        _ocassionRepository = ocassionRepository;
    }

    public async Task<Output> Execute(IInviteUserDTO data)
    {
        var ocassion = await getOcassionOrThrow(data);

        await sendInvitation(ocassion, data.Receiver);

        return presentResult(ocassion);
    }


    private async Task<Ocassion> getOcassionOrThrow(IInviteUserDTO data)
    {
        var ocassion = await _ocassionRepository.Find(data.OcassionId);

        if (ocassion == null)
        {
            throw new OcassionDoesNotExistException();
        }

        return ocassion;
    }

    private async Task sendInvitation(Ocassion ocassion, string receiver)
    {
        var message = MessageFactory.CreateInvitation(receiver, ocassion.Description);
        await _notifier.Notify(message);
    }

    private Output presentResult(Ocassion ocassion)
    {
        var dto = OcassionDTO.From(ocassion!);
        return _presenter.Present(dto);
    }
}