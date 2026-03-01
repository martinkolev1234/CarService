using CarService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.DL.Interfaces;
using CarService.DL.LocalDb;



namespace CarService.DL.Repositories
{
    public class CustomerLocalRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            StaticDb.Customers.Add(customer);
        }

        public void DeleteCustomer(Guid id)
        {
            StaticDb.Customers.RemoveAll(c => c.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return StaticDb.Customers;
        }

        public Customer? GetById(Guid id)
        {
            return StaticDb.Customers
                .FirstOrDefault(c => c.Id == id);
        }
    }
}

