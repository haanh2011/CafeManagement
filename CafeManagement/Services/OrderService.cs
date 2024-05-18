using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class OrderService
    {
        private Models.LinkedList<Order> _orders;
        private readonly string _filePath;

        public OrderService(string filePath)
        {
            _filePath = filePath;
            _orders = DataManager.LoadOrders(_filePath);
        }

        public Models.LinkedList<Order> GetAllItems()
        {
            _orders = DataManager.LoadOrders(_filePath);
            return _orders;
        }

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
        public Order GetById(int orderId)
        {
            return _orders.Find(p => p.Id == orderId)?.Data;
        }

    }
}
