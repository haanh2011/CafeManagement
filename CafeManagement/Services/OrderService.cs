using System;
using System.Collections.Generic;
using CafeManagement.Data;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class OrderService
    {
        private readonly string _filePath;
        private readonly List<Order> _orders;

        public OrderService(string filePath, List<Order> orders)
        {
            _filePath = filePath;
            _orders = orders;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
            DataManager.SaveOrders(_filePath, _orders);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                if (_orders[i].Id == updatedOrder.Id)
                {
                    _orders[i] = updatedOrder;
                    break;
                }
            }
            DataManager.SaveOrders(_filePath, _orders);
        }

        public void DeleteOrder(int orderId)
        {
            _orders.RemoveAll(o => o.Id == orderId);
            DataManager.SaveOrders(_filePath, _orders);
        }
    }
}
