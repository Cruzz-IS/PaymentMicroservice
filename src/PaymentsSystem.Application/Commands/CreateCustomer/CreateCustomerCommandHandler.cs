using MediatR;
using PaymentsSystem.Application.DTOs;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Domain.Entities;
using PaymentsSystem.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            var emailExists = await _customerRepository
                .ExistsByEmailAsync(request.Email, cancellationToken);

            if (emailExists)
                throw new InvalidOperationException($"Ya existe un cliente con el email {request.Email}.");

            var customer = new Customer(request.FullName, request.Email, request.Phone);

            await _customerRepository.AddAsync(customer, cancellationToken);
            await _customerRepository.SaveChangesAsync(cancellationToken);

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
