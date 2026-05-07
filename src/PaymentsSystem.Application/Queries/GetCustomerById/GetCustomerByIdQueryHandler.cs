using MediatR;
using PaymentsSystem.Application.DTOs;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository
                .GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                throw new CustomerNotFoundException(request.CustomerId);

            return new CustomerDto(
                customer.Id,
                customer.FullName,
                customer.Email,
                customer.Phone,
                customer.IsActive,
                customer.CreatedAt
            );
        }
    }
}
