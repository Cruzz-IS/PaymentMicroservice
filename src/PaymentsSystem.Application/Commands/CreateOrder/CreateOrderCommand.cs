using MediatR;
using PaymentsSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Commands.CreateOrder
{
    public record OrderItemRequest(
    string ProductName,
    int Quantity,
    decimal UnitPrice
);

    public record CreateOrderCommand(
        Guid CustomerId,
        string Currency,
        List<OrderItemRequest> Items
    ) : IRequest<OrderDto>;
}
