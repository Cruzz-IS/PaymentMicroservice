using MediatR;
using PaymentsSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Queries.GetCustomerById
{
    public record GetCustomerByIdQuery(Guid CustomerId) : IRequest<CustomerDto>;
}
