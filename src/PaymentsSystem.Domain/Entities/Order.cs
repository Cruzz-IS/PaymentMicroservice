using PaymentsSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentsSystem.Domain.Events;

namespace PaymentsSystem.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Customer? Customer { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private Order()
        {
            Currency = string.Empty;
        }

        public Order(Guid customerId, string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("La moneda es obligatoria.");

            Id = Guid.NewGuid();
            CustomerId = customerId;
            Currency = currency.ToUpper();
            Status = OrderStatus.Pending;
            TotalAmount = 0;
            CreatedAt = DateTime.UtcNow;
        }

        // Agrega un item y recalcula el total del stock automaticamente
        public void AddItem(string productName, int quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException(
                    "Solo se pueden agregar items a órdenes en estado Pending.");

            var item = new OrderItem(Id, productName, quantity, unitPrice);
            _items.Add(item);
            RecalculateTotal();
            UpdatedAt = DateTime.UtcNow;
        }

        // Confirma la orden cuando el pago fue procesado correctamente y exitoso
        public void Confirm()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException(
                    $"No se puede confirmar una orden en estado {Status}.");

            if (!_items.Any())
                throw new InvalidOperationException(
                    "No se puede confirmar una orden sin items.");

            Status = OrderStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;

            // Registra el evento — se publicará a RabbitMQ después de guardar en BD
            _domainEvents.Add(new OrderCreatedEvent(Id, CustomerId, TotalAmount, Currency));
        }

        // Cancela una orden
        public void Cancel()
        {
            if (Status == OrderStatus.Confirmed)
                throw new InvalidOperationException(
                    "No se puede cancelar una orden ya confirmada. Usa un reembolso.");

            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("La orden ya está cancelada.");

            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        // Marcar una orden como reembolsada
        public void MarkAsRefunded()
        {
            if (Status != OrderStatus.Confirmed)
                throw new InvalidOperationException(
                    "Solo se pueden reembolsar órdenes confirmadas.");

            Status = OrderStatus.Refunded;
            UpdatedAt = DateTime.UtcNow;
        }

        // Limpia los eventos después de publicarlos — el Handler los procesa y los borra
        public void ClearDomainEvents() => _domainEvents.Clear();

        private void RecalculateTotal()
        {
            TotalAmount = _items.Sum(i => i.Subtotal);
        }
    }
    //[Table("Orders")]
    //public class Order
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    // Auditoría
    //    [Required]
    //    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //    public DateTime? UpdatedAt { get; set; }

    //    [StringLength(50)]
    //    public string? CreatedBy { get; set; }

    //    [StringLength(50)]
    //    public string? UpdatedBy { get; set; }
    //}
}
