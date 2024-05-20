using System;

namespace CafeManagement.Models
{
    /// <summary>
    /// Định nghĩa một đối tượng khách hàng trong hệ thống quản lý quán cà phê.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Mã số duy nhất của khách hàng.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên của khách hàng.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ngày sinh của khách hàng.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Địa chỉ email của khách hàng.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại của khách hàng.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Số điểm tích lũy của khách hàng.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp Customer với các thông tin cơ bản của khách hàng.
        /// </summary>
        /// <param name="name">Tên của khách hàng.</param>
        /// <param name="birthday">Ngày sinh của khách hàng.</param>
        /// <param name="phoneNumber">Số điện thoại của khách hàng.</param>
        /// <param name="email">Địa chỉ email của khách hàng.</param>
        /// <param name="id">Mã số của khách hàng (mặc định là 0 nếu không có giá trị được cung cấp).</param>
        /// <param name="points">Số điểm tích lũy của khách hàng (mặc định là 0 nếu không có giá trị được cung cấp).</param>
        public Customer(string name, DateTime birthday, string phoneNumber, string email, int id = 0, int points = 0)
        {
            Id = id;
            Name = name;
            Birthday = birthday;
            Email = email;
            PhoneNumber = phoneNumber;
            Points = points;
        }

        /// <summary>
        /// Cộng thêm điểm tích lũy cho khách hàng.
        /// </summary>
        /// <param name="points">Số điểm cần thêm vào điểm tích lũy của khách hàng.</param>
        public void AddPoints(int points)
        {
            Points += points;
        }

        /// <summary>
        /// Trả về một chuỗi biểu diễn của đối tượng Customer.
        /// </summary>
        /// <returns>Chuỗi biểu diễn của đối tượng Customer.</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Tên: {Name}, Ngày sinh: {Birthday}, Email: {Email}, SĐT: {PhoneNumber}, Điểm tích lũy: {Points}";
        }
    }
}
