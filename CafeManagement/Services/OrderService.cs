using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class OrderService
    {
        private string _filePath;
        private List<Order> _orders;

        public OrderService(string filePath)
        {
            _filePath = filePath;
            _orders = DataManager.LoadOrders(_filePath);
        }

        public List<Order> GetAllItems()
        {
            return _orders;
        }

        public void DisplayAllItems()
        {
            foreach (var order in _orders)
            {
                Console.WriteLine($"Đơn hàng ID: {order.Id}");
                Console.WriteLine($"Mã khách hàng: {order.CustomerId}");
                Console.WriteLine($"Ngày đặt hàng: {order.OrderDate:yyyy-MM-dd}");
                Console.WriteLine("Sản phẩm trong đơn hàng:");

                foreach (var item in order.Items)
                {
                    Console.WriteLine($"- Mã sản phẩm: {item.ProductId}, Số lượng: {item.Quantity}, Giá đơn vị: {item.UnitPrice}, Tổng giá: {item.TotalPrice()}");
                }
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public void Add(Order order)
        {
            // Tìm mã số lớn nhất hiện tại
            int maxId = _orders.Count > 0 ? _orders.Max(o => o.Id) : 0;

            // Gán mã số mới cho đơn hàng
            order.Id = maxId + 1;

            // Thêm đơn hàng mới vào danh sách
            _orders.Add(order);

            // Lưu danh sách đơn hàng vào file
            DataManager.SaveOrders(_filePath, _orders);
        }

        public void Update(Order updatedOrder)
        {
            var order = _orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (order != null)
            {
                order.CustomerId = updatedOrder.CustomerId;
                order.OrderDate = updatedOrder.OrderDate;
                order.Items = updatedOrder.Items;

                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được cập nhật.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
        }

        public void Delete(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                _orders.Remove(order);
                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
        }
        public Order GetById(int orderId)
        {
            return _orders.FirstOrDefault(p => p.Id == orderId);
        }
    }
}
