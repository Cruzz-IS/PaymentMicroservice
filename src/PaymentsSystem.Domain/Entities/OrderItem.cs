using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Subtotal => Quantity * UnitPrice; // Calculado, no se guarda en la base de datos

        private OrderItem()
        {
            ProductName = string.Empty;
        }

        public OrderItem(Guid orderId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");

            if (unitPrice <= 0)
                throw new ArgumentException("El precio unitario debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
