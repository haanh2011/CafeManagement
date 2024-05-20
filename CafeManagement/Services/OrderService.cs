using System;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class OrderService
    {
        private LinkedList<Order> _orders;
        private readonly string _filePath;

        /// <summary>
        /// Khởi tạo một đối tượng dịch vụ đơn hàng mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp lưu trữ đơn hàng.</param>
        public OrderService(string filePath)
        {
            _filePath = filePath;
            _orders = DataManager.LoadOrders(_filePath);
        }

        /// <summary>
        /// Lấy danh sách tất cả đơn hàng.
        /// </summary>
        /// <returns>Danh sách tất cả đơn hàng.</returns>
        public LinkedList<Order> GetAllItems()
        {
            _orders = DataManager.LoadOrders(_filePath);
            return _orders;
        }

        /// <summary>
        /// Thêm một đơn hàng mới.
        /// </summary>
        /// <param name="order">Đơn hàng cần thêm.</param>
        /// <returns>Đơn hàng đã được thêm vào danh sách.</returns>
        public Order Add(Order order)
        {
            // Tìm mã số lớn nhất hiện tại
            Order orderMax = _orders.Max(o => o.Id);
            int maxId = _orders.Count > 0 ? orderMax.Id : 0;

            // Gán mã số mới cho đơn hàng
            order.Id = maxId + 1;

            // Thêm đơn hàng mới vào danh sách
            _orders.AddLast(order);

            // Lưu danh sách đơn hàng vào file
            DataManager.SaveOrders(_filePath, _orders);
            return order;
        }

        /// <summary>
        /// Cập nhật thông tin một đơn hàng.
        /// </summary>
        /// <param name="updatedOrder">Đơn hàng cần cập nhật thông tin.</param>
        public void Update(Order updatedOrder)
        {
            Node<Order> order = _orders.Find(p => p.Id == updatedOrder.Id);
            if (order != null)
            {
                order.Data.CustomerId = updatedOrder.CustomerId;
                order.Data.OrderDate = updatedOrder.OrderDate;
                order.Data.Items = updatedOrder.Items;

                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
        }

        /// <summary>
        /// Xóa một đơn hàng dựa trên ID.
        /// </summary>
        /// <param name="orderId">ID của đơn hàng cần xóa.</param>
        public void Delete(int orderId)
        {
            Node<Order> order = _orders.Find(p => p.Id == orderId);
            if (order != null)
            {
                _orders.RemoveNode(order);
                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
        }

        /// <summary>
        /// Lấy thông tin của một đơn hàng dựa trên ID.
        /// </summary>
        /// <param name="orderId">ID của đơn hàng cần lấy thông tin.</param>
        /// <returns>Đơn hàng thỏa mãn ID hoặc null nếu không tìm thấy.</returns>
        public Order GetById(int orderId)
        {
            return _orders.Find(p => p.Id == orderId)?.Data;
        }
    }
}
