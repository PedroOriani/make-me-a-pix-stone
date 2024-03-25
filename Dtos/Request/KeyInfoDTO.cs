namespace Pix.DTOs
{
    public class KeyInfoDto
    {
        public required KeyDto Key { get; set; }
        public required UserDto User { get; set; }
        public required AccountDto Account { get; set; }
    }

    public class KeyDto
    {
        public required string Value { get; set; }
        public required string Type { get; set; }
    }

    public class UserDto
    {
        public required string Name { get; set; }
        public required string MaskedCpf { get; set; }
    }

    public class AccountDto
    {
        public required string Number { get; set; }
        public required string Agency { get; set; }
        public required string BankName { get; set; }
        public required int BankId { get; set; }
    }
}
