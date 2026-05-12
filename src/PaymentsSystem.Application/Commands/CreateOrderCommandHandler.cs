using MediatR;
using PaymentsSystem.Application.Commands.CreateOrder;
using PaymentsSystem.Application.DTOs;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Domain.Entities;
using PaymentsSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDto> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            // Verifica que el cliente existe antes de crear la orden
            var customer = await _customerRepository
                .GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                throw new CustomerNotFoundException(request.CustomerId);

            if (!customer.IsActive)
                throw new InvalidOperationException(
                    "No se puede crear una orden para un cliente inactivo.");

            var order = new Order(request.CustomerId, request.Currency);

            foreach (var item in request.Items)
                order.AddItem(item.ProductName, item.Quantity, item.UnitPrice);

            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return MapToDto(order, customer.FullName);
        }

        private static OrderDto MapToDto(Order order, string customerName) =>
            new(
                order.Id,
                order.CustomerId,
                customerName,
                order.Status.ToString(),
                order.TotalAmount,
                order.Currency,
                order.CreatedAt,
                order.Items.Select(i => new OrderItemDto(
                    i.Id,
                    i.ProductName,
                    i.Quantity,
                    i.UnitPrice,
                    i.Subtotal
                )).ToList().AsReadOnly()
            );
    }
}
