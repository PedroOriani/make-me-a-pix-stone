using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pix.Models
{
    public enum PaymentStatus
    {
        PROCESSING,
        FAILED,
        SUCCESS
    }

    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.PROCESSING;

        public int Amount { get; set; }

        public string? Description { get; set; }

        public int PixKeyId { get; set; }

        [ForeignKey("PixKeyId")]
        public Key? PixKey { get; set; }

        public int PaymentProviderAccountId { get; set; }

        [ForeignKey("PaymentProviderAccountId")]
        public Account? PaymentProviderAccount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
