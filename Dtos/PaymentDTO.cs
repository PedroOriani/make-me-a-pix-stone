namespace Pix.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public required OriginDto Origin { get; set; }
        public required DestinyDto Destiny { get; set; }
        public required int Amount { get; set; }
        public string? Description { get; set; }
    }
}

