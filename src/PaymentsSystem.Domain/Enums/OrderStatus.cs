using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,      // Orden creada,
        Confirmed = 2,    // Pago recibido, orden confirmada
        Cancelled = 3,    // Orden cancelada
        Refunded = 4      // Orden reembolsada
    }
}
