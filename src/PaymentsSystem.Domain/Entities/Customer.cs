using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Domain.Entities
{
    [Table("Customers")]
    internal class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 80 caracteres")]
        public string FullName { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "El nombre de usuario debe tener entre 2 y 20 caracteres")]
        public string? Username { get; set; }

        // Relación con User
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

    }
}
