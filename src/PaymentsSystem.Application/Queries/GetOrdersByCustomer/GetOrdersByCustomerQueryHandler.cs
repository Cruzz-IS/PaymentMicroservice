using MediatR;
using PaymentsSystem.Application.DTOs;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler
    : IRequestHandler<GetOrdersByCustomer, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetOrdersByCustomerQueryHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<OrderDto>> Handle(
            GetOrdersByCustomer request,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository
                .GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                throw new CustomerNotFoundException(request.CustomerId);

            var orders = await _orderRepository
                .GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            return orders.Select(order => new OrderDto(
                order.Id,
                order.CustomerId,
                customer.FullName,
                order.Status.ToString(),
                order.TotalAmount,
                order.Currency,
                order.CreatedAt,
                order.Items.Select(i => new OrderItemDto(
                    i.Id, i.ProductName, i.Quantity, i.UnitPrice, i.Subtotal
                )).ToList().AsReadOnly()
            ));
        }
    }
}
