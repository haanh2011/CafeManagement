using System;

namespace CafeManagement.Models
{
    /// <summary>
    /// Định nghĩa một đối tượng hóa đơn trong hệ thống quản lý quán cà phê.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Mã số duy nhất của hóa đơn.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Mã số đơn hàng liên kết với hóa đơn.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Ngày tạo hóa đơn.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp Invoice với các thông tin cơ bản của hóa đơn.
        /// </summary>
        /// <param name="id">Mã số của hóa đơn.</param>
        /// <param name="orderId">Mã số đơn hàng liên kết với hóa đơn.</param>
        /// <param name="date">Ngày tạo hóa đơn.</param>
        public Invoice(int id, int orderId, DateTime date)
        {
            Id = id;
            OrderId = orderId;
            Date = date;
        }

        /// <summary>
        /// Trả về một chuỗi biểu diễn của đối tượng Invoice.
        /// </summary>
        /// <returns>Chuỗi biểu diễn của đối tượng Invoice.</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Mã Đơn Hàng: {OrderId}, Ngày tạo hoá đơn: {Date.ToShortDateString()}";
        }
    }
}
