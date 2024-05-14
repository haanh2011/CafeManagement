using System;
using System.Collections.Generic;

namespace CafeManagement.Models
{
    public class Order
    {
        public int Id { get; set; }                  // Mã đơn hàng
        public int CustomerId { get; set; }        // Thông tin khách hàng
        public DateTime OrderDate { get; set; }      // Ngày đặt hàng
        public List<OrderItem> Items { get; set; }   // Danh sách sản phẩm trong đơn hàng

        public Order(int id, int customerId, DateTime orderDate, List<OrderItem> items)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            Items = items;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Customer ID: {CustomerId}, OrderDate: {OrderDate}, Items: {string.Join(", ", Items)}";
        }
    }

    public class OrderItem
    {
        public int ProductId { get; set; } // Sản phẩm
        public int Quantity { get; set; }    // Số lượng
        public decimal UnitPrice { get; set; } // Giá sản phẩm

        public OrderItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal TotalPrice()
        {
            return Quantity * UnitPrice;
        }

        public override string ToString()
        {
            return $"ProductId: {ProductId}, Số lượng: {Quantity}, Giá:${UnitPrice}";
        }
    }
}
