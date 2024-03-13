using System.ComponentModel.DataAnnotations;

namespace Pix.DTOs
{
    public class CreateKeyDTO
    {
        public KeyRequest Key { get; set; }
        public UserRequest User { get; set; }
        public AccountRequest Account { get; set; }
    }

    public class KeyRequest
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public string Type { get; set; }
    }

    public class UserRequest
    {
        [Required]
        public string Cpf { get; set; }
    }

    public class AccountRequest
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Agency { get; set; }
    }
}
