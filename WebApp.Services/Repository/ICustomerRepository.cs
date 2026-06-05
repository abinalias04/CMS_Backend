using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Entity.Models;

namespace WebApp.Services.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(Guid id);

        Task<Customer> CreateAsync(Customer customer);

        Task<Customer> UpdateAsync(Customer customer);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> EmailExistsAsync(string email);
    }
}
