using Pix.DTOs;
using Pix.Exceptions;

namespace Pix.Services;

public class ConcilliationService(MessageService messageService)
{
    private readonly MessageService _messageService = messageService;

    public async Task Verify(ConcilliationDTO dto, int bankId)
    {
        if (dto.Date > DateTime.Today) throw new DateGraterThanTodayException("You can't put a future data for this route");

        PostConcilliationDTO concilliationDTO = new(dto, bankId);

        _messageService.SendMessage(concilliationDTO, "concilliations");

        await Task.CompletedTask;
    }
}