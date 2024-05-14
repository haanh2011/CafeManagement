using System.Collections.Generic;
using CafeManagement.Data;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CustomerService
    {
        private readonly string _filePath;
        private readonly List<Customer> _customers;
        public CustomerService(string filePath, List<Customer> customers)
        {
            _filePath = filePath;
            _customers = customers;
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
            DataManager.SaveCustomers(_filePath, _customers);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            for (int i = 0; i < _customers.Count; i++)
            {
                if (_customers[i].Id == updatedCustomer.Id)
                {
                    _customers[i] = updatedCustomer;
                    break;
                }
            }
            DataManager.SaveCustomers(_filePath, _customers);
        }

        public void DeleteCustomer(int customerId)
        {
            _customers.RemoveAll(c => c.Id == customerId);
            DataManager.SaveCustomers(_filePath, _customers);
        }

        public void UpdatePoints(Customer customer, int points)
        {
            customer.Points += points;
        }
    }
}
