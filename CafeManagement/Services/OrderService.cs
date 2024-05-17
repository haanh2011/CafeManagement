using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class OrderService
    {
        private List<Order> _orders;
        private readonly string _filePath;

        public OrderService(string filePath)
        {
            _filePath = filePath;
            _orders = DataManager.LoadOrders(_filePath);
        }

        public List<Order> GetAllItems()
        {
            _orders = DataManager.LoadOrders(_filePath);
            return _orders;
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
            if (order.Equals(default(Order)))
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
            else
            {
                order.CustomerId = updatedOrder.CustomerId;
                order.OrderDate = updatedOrder.OrderDate;
                order.Items = updatedOrder.Items;

                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được cập nhật.");
            }
        }

        public void Delete(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order.Equals(default(Order)))
            {
                Console.WriteLine("Không tìm thấy đơn hàng với mã số đó.");
            }
            else
            {
                _orders.Remove(order);
                DataManager.SaveOrders(_filePath, _orders);
                Console.WriteLine("Đơn hàng đã được xóa.");
            }
        }
        public Order GetById(int orderId)
        {
            return _orders.FirstOrDefault(p => p.Id == orderId);
        }

    }
}
