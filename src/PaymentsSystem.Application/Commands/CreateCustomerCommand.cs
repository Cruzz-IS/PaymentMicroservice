using MediatR;
using PaymentsSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.Commands
{
    public record CreateCustomerCommand(
    string FullName,
    string Email,
    string? Phone
    ) : IRequest<CustomerDto>;
}
