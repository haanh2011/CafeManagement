using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CustomerService
    {
        private List<Customer> _customers;
        private readonly string _filePath;

        public CustomerService(string filePath)
        {
            _filePath = filePath;
            _customers = DataManager.LoadCustomers(filePath); // Tải danh sách khách hàng từ tệp
        }

        public List<Customer> GetAllItems()
        {
            return _customers; // Trả về danh sách tất cả khách hàng
        }
        public void AddPoints(int points)
        {
            // Lấy danh sách khách hàng
            var customers = GetAllItems();

            // Giảm số điểm từ tổng tiền
            foreach (var customer in customers)
            {
                customer.AddPoints(points);
            }

            // Lưu danh sách khách hàng với điểm mới
            DataManager.SaveCustomers(_filePath, customers);
        }

        public int Add(Customer customer)
        {
            int maxId = _customers.Count > 0 ? _customers.Max(c => c.Id) : 0;
            customer.Id = maxId + 1;

            _customers.Add(customer); // Thêm khách hàng mới vào danh sách

            DataManager.SaveCustomers(_filePath, _customers); // Lưu danh sách khách hàng vào tệp
            return customer.Id;
        }

        public void Update(Customer updatedCustomer)
        {
            int index = _customers.FindIndex(c => c.Id == updatedCustomer.Id);
            if (index != -1)
            {
                _customers[index] = updatedCustomer; // Cập nhật thông tin khách hàng

                DataManager.SaveCustomers(_filePath, _customers); // Lưu danh sách khách hàng vào tệp
            }
            else
            {
                throw new InvalidOperationException("Không tìm thấy khách hàng."); // Ném ngoại lệ nếu không tìm thấy khách hàng
            }
        }

        public void Delete(int customerId)
        {
            Customer customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer.Equals(default(Customer)))
            {
                throw new InvalidOperationException("Không tìm thấy khách hàng."); // Ném ngoại lệ nếu không tìm thấy khách hàng
            }
            else
            {
                _customers.Remove(customer); // Xóa khách hàng
                DataManager.SaveCustomers(_filePath, _customers); // Lưu danh sách khách hàng vào tệp
            }
        }

        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id); // Trả về khách hàng theo ID
        }

        public Customer GetByPhoneNumber(string phoneNumber)
        {
            return _customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber.Trim()); // Trả về khách hàng theo Phone Number
        }
    }
}
