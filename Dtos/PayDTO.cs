namespace Pix.DTOs
{
    public class PayDTO
    {
        public required OriginDto Origin { get; set; }

        public required DestinyDto Destiny { get; set; }

        public required int Amount { get; set; }

        public string? Description { get; set; }

    }

    public class OriginDto
    {
        public required UserPayDto User { get; set; }

        public required AccountPayDto Account { get; set; }
    }

    public class UserPayDto
    {
        public required string Cpf { get; set; }
    }

    public class AccountPayDto
    {
        public required string Number { get; set; }

        public required string Agency { get; set; }   
    }


    public class DestinyDto
    {
        public required KeyDestinyDto Key { get; set; }
    }

    public class KeyDestinyDto
    {
        public required string Value { get; set; }

        public required string Type { get; set; }
    }
}