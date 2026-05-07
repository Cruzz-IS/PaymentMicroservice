using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(Guid customerId)
            : base($"El cliente con ID {customerId} no fue encontrado.")
        {
        }

        public CustomerNotFoundException(string email)
            : base($"El cliente con email {email} no fue encontrado.")
        {
        }
    }
}
