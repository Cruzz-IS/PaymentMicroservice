using Microsoft.EntityFrameworkCore;
using PaymentsSystem.Application.Interfaces;
using PaymentsSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsSystem.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _context.Customers
                .Where(c => c.IsActive)
                .OrderBy(c => c.FullName)
                .ToListAsync(cancellationToken);

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
            => await _context.Customers
                .AnyAsync(c => c.Email == email, cancellationToken);

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
            => await _context.Customers.AddAsync(customer, cancellationToken);

        public Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _context.Customers.Update(customer);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
