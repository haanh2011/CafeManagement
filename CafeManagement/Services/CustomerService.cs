using System;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CustomerService
    {
        private LinkedList<Customer> _customerService;
        private readonly string _filePath;

        public CustomerService(string filePath)
        {
            _filePath = filePath;
            _customerService = DataManager.LoadCustomers(filePath); // Tải danh sách khách hàng từ tệp
        }

        public LinkedList<Customer> GetAllItems()
        {
            return _customerService; // Trả về danh sách tất cả khách hàng
        }

        public void AddPoints(int points)
        {
            // Lấy danh sách khách hàng
            LinkedList<Customer> customers = GetAllItems();

            // Giảm số điểm từ tổng tiền
            foreach (Customer customer in customers.ToList())
            {
                customer.AddPoints(points);
            }

            // Lưu danh sách khách hàng với điểm mới
            DataManager.SaveCustomers(_filePath, customers);
        }

        public Customer Add(Customer customer)
        {
            Customer customerMax = _customerService.Max(c => c.Id);
            int maxId = _customerService.Count > 0 ? customerMax.Id : 0;
            customer.Id = maxId + 1;

            _customerService.AddLast(customer); // Thêm khách hàng mới vào danh sách

            DataManager.SaveCustomers(_filePath, _customerService); // Lưu danh sách khách hàng vào tệp
            return customer;
        }

        public void Update(Customer updatedCustomer)
        {
            Node<Customer> customer = _customerService.Find(c => c.Id == updatedCustomer.Id);
            if (customer !=null)
            {
                customer.Data = updatedCustomer; // Cập nhật thông tin khách hàng

                DataManager.SaveCustomers(_filePath, _customerService); // Lưu danh sách khách hàng vào tệp
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng.");
            }
        }

        public void Delete(int customerId)
        {
            Node<Customer> customer = _customerService.Find(c => c.Id == customerId);
            if (customer != null)
            {
                _customerService.RemoveNode(customer); // Xóa khách hàng
                DataManager.SaveCustomers(_filePath, _customerService); // Lưu danh sách khách hàng vào tệp
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng.");
            }
        }

        public Customer GetById(int id)
        {
            return _customerService.Find(c => c.Id ==  id)?.Data; // Trả về khách hàng theo ID
        }

        public Customer GetByPhoneNumber(string phoneNumber)
        {
            return _customerService.Find(c => c.PhoneNumber ==  phoneNumber.Trim())?.Data; // Trả về khách hàng theo Phone Number
        }
    }
}
