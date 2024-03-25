using System.ComponentModel.DataAnnotations;

namespace Pix.DTOs
{
    public class CreateKeyDTO
    {
        public required KeyRequest Key { get; set; }
        public required UserRequest User { get; set; }
        public required AccountRequest Account { get; set; }
    }

    public class KeyRequest
    {
        [Required]
        public required string Value { get; set; }

        [Required]
        public required string Type { get; set; }
    }

    public class UserRequest
    {
        [Required]
        public required string Cpf { get; set; }
    }

    public class AccountRequest
    {
        [Required]
        public required string Number { get; set; }

        [Required]
        public required string Agency { get; set; }
    }
}
