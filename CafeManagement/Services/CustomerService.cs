using System;
using CafeManagement.Manager;
using CafeManagement.Models;
using CafeManagement.Utilities;

namespace CafeManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý khách hàng.
    /// </summary>
    public class CustomerService
    {
        public LinkedList<Customer> Customers; // Danh sách các khách hàng
        private readonly string _filePath; // Đường dẫn đến tệp danh sách khách hàng

        /// <summary>
        /// Khởi tạo một đối tượng CustomerService mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp danh sách khách hàng.</param>
        public CustomerService(string filePath)
        {
            _filePath = filePath;
            Customers = DataManager.LoadCustomers(_filePath); // Tải danh sách khách hàng từ tệp
        }

        /// <summary>
        /// Lấy tất cả các khách hàng.
        /// </summary>
        /// <returns>Danh sách các khách hàng.</returns>
        public void GetAllItems()
        {
            Customers = DataManager.LoadCustomers(_filePath); // Tải danh sách khách hàng từ tệp
        }

        /// <summary>
        /// Thêm điểm cho tất cả các khách hàng.
        /// </summary>
        /// <param name="points">Số điểm được thêm cho mỗi khách hàng.</param>
        public void AddPoints(int updatedCustomerId, int points)
        {
            // Thêm điểm cho khách hàng
           
            Node<Customer> customer = Customers.Find(c => c.Id == updatedCustomerId);
            if (customer != null)
            {
                // Cập nhật thông tin khách hàng
                customer.Data.AddPoints(points);
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng.");
            }
            // Lưu danh sách khách hàng với điểm mới
            DataManager.SaveCustomers(_filePath, Customers);
        }

        /// <summary>
        /// Thêm một khách hàng mới.
        /// </summary>
        /// <param name="customer">Khách hàng cần thêm.</param>
        /// <returns>Khách hàng đã được thêm.</returns>
        public Customer Add(Customer customer)
        {
            Customer customerMax = Customers.Max(c => c.Id);
            int maxId = Customers.Count > 0 ? customerMax.Id : 0;
            customer.Id = maxId + 1;

            Customers.AddLast(customer); // Thêm khách hàng mới vào danh sách

            DataManager.SaveCustomers(_filePath, Customers); // Lưu danh sách khách hàng vào tệp
            return customer;
        }

        /// <summary>
        /// Cập nhật thông tin của một khách hàng.
        /// </summary>
        /// <param name="updatedCustomer">Khách hàng cần cập nhật.</param>
        public void Update(Customer updatedCustomer)
        {
            Node<Customer> customer = Customers.Find(c => c.Id == updatedCustomer.Id);
            if (customer != null)
            {
                customer.Data = updatedCustomer; // Cập nhật thông tin khách hàng

                DataManager.SaveCustomers(_filePath, Customers); // Lưu danh sách khách hàng vào tệp
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
            Node<Customer> customer = Customers.Find(c => c.Id == customerId);
            if (customer != null)
            {
                Customers.RemoveNode(customer); // Xóa khách hàng
                DataManager.SaveCustomers(_filePath, Customers); // Lưu danh sách khách hàng vào tệp
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
            return Customers.Find(c => c.Id == id)?.Data; // Trả về khách hàng theo ID
        }

        /// <summary>
        /// Lấy thông tin của một khách hàng dựa trên số điện thoại.
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại của khách hàng cần tìm.</param>
        /// <returns>Khách hàng thỏa mãn số điện thoại hoặc null nếu không tìm thấy.</returns>
        public Customer GetByPhoneNumber(string phoneNumber)
        {
            return Customers.Find(c => c.PhoneNumber == phoneNumber.Trim())?.Data; // Trả về khách hàng theo số điện thoại
        }
    }
}
