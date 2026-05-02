using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Domain.Entities
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "la divisa debe tener entre 2 y 20 caracteres")]
        public string? Currency { get; set; }

        public bool? Status { get; set; }

        public string? Provider { get; set; }

        // Relación con User
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;

    }
}
