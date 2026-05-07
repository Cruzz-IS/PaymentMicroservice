using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Application.DTOs
{
    public record CustomerDto(
        Guid Id,
        string FullName,
        string Email,
        string? Phone,
        bool IsActive,
        DateTime CreatedAt
    );
}
