using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaymentsSystem.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string? Phone { get; private set; }
        public string? StripeCustomerId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Customer()
        {
            FullName = string.Empty;
            Email = string.Empty;
        }

        public Customer(string fullName, string email, string? phone = null)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Phone = phone;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string fullName, string? phone)
        {
            FullName = fullName;
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignStripeCustomerId(string stripeCustomerId)
        {
            StripeCustomerId = stripeCustomerId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}




//namespace PaymentsSystem.Domain.Entities
//{
//    [Table("Customers")]
//    public class Customer
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        [Required(ErrorMessage = "El nombre es requerido")]
//        [StringLength(80, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 80 caracteres")]
//        public string FullName { get; set; } = string.Empty;

//        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
//        [StringLength(20)]
//        public string? PhoneNumber { get; set; }

//        [StringLength(20, MinimumLength = 2, ErrorMessage = "El nombre de usuario debe tener entre 2 y 20 caracteres")]
//        public string? Username { get; set; }

//        // Relación con User
//        [ForeignKey("UserId")]
//        public virtual User User { get; set; } = null!;

//    }
//}
