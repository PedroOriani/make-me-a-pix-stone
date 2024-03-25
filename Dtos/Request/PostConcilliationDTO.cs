namespace Pix.DTOs;

public class PostConcilliationDTO(ConcilliationDTO dto, int bankId)
{
    public DateTime Date { get; } = dto.Date;
    public string File { get; } = dto.File;
    public string Postback { get; } = dto.Postback;
    public int BankId { get; } = bankId;
}