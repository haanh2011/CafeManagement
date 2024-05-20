using System;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý khách hàng.
    /// </summary>
    public class CustomerService
    {
        private LinkedList<Customer> _customerService; // Danh sách các khách hàng
        private readonly string _filePath; // Đường dẫn đến tệp danh sách khách hàng

        /// <summary>
        /// Khởi tạo một đối tượng CustomerService mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp danh sách khách hàng.</param>
        public CustomerService(string filePath)
        {
            _filePath = filePath;
            _customerService = DataManager.LoadCustomers(filePath); // Tải danh sách khách hàng từ tệp
        }

        /// <summary>
        /// Lấy tất cả các khách hàng.
        /// </summary>
        /// <returns>Danh sách các khách hàng.</returns>
        public LinkedList<Customer> GetAllItems()
        {
            return _customerService; // Trả về danh sách tất cả khách hàng
        }

        /// <summary>
        /// Thêm điểm cho tất cả các khách hàng.
        /// </summary>
        /// <param name="points">Số điểm được thêm cho mỗi khách hàng.</param>
        public void AddPoints(int points)
        {
            // Lấy danh sách khách hàng
            LinkedList<Customer> customers = GetAllItems();

            // Thêm điểm cho từng khách hàng
            foreach (Customer customer in customers.ToList())
            {
                customer.AddPoints(points);
            }

            // Lưu danh sách khách hàng với điểm mới
            DataManager.SaveCustomers(_filePath, customers);
        }

        /// <summary>
        /// Thêm một khách hàng mới.
        /// </summary>
        /// <param name="customer">Khách hàng cần thêm.</param>
        /// <returns>Khách hàng đã được thêm.</returns>
        public Customer Add(Customer customer)
        {
            Customer customerMax = _customerService.Max(c => c.Id);
            int maxId = _customerService.Count > 0 ? customerMax.Id : 0;
            customer.Id = maxId + 1;

            _customerService.AddLast(customer); // Thêm khách hàng mới vào danh sách

            DataManager.SaveCustomers(_filePath, _customerService); // Lưu danh sách khách hàng vào tệp
            return customer;
        }

        /// <summary>
        /// Cập nhật thông tin của một khách hàng.
        /// </summary>
        /// <param name="updatedCustomer">Khách hàng cần cập nhật.</param>
        public void Update(Customer updatedCustomer)
        {
            Node<Customer> customer = _customerService.Find(c => c.Id == updatedCustomer.Id);
            if (customer != null)
            {
                customer.Data = updatedCustomer; // Cập nhật thông tin khách hàng

                DataManager.SaveCustomers(_filePath, _customerService); // Lưu danh sách khách hàng vào tệp
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng.");
            }
        }

        /// <summary>
        /// Xóa một khách hàng dựa trên mã số.
        /// </summary>
        /// <param name="customerId">Mã số của khách hàng cần xóa.</param>
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

        /// <summary>
        /// Lấy thông tin của một khách hàng dựa trên mã số.
        /// </summary>
        /// <param name="id">Mã số của khách hàng cần lấy.</param>
        /// <returns>Khách hàng thỏa mãn mã số hoặc null nếu không tìm thấy.</returns>
        public Customer GetById(int id)
        {
            return _customerService.Find(c => c.Id == id)?.Data; // Trả về khách hàng theo ID
        }

        /// <summary>
        /// Lấy thông tin của một khách hàng dựa trên số điện thoại.
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại của khách hàng cần tìm.</param>
        /// <returns>Khách hàng thỏa mãn số điện thoại hoặc null nếu không tìm thấy.</returns>
        public Customer GetByPhoneNumber(string phoneNumber)
        {
            return _customerService.Find(c => c.PhoneNumber == phoneNumber.Trim())?.Data; // Trả về khách hàng theo số điện thoại
        }
    }
}
