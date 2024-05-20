using System;

namespace CafeManagement.Models
{
    /// <summary>
    /// Đại diện cho một đơn hàng trong cửa hàng.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Mã đơn hàng.
        /// </summary>
        public int Id { get; set; }                  // Mã đơn hàng

        /// <summary>
        /// Thông tin khách hàng.
        /// </summary>
        public int CustomerId { get; set; }        // Thông tin khách hàng

        /// <summary>
        /// Ngày đặt hàng.
        /// </summary>
        public DateTime OrderDate { get; set; }      // Ngày đặt hàng

        /// <summary>
        /// Danh sách sản phẩm trong đơn hàng.
        /// </summary>
        public LinkedList<OrderItem> Items { get; set; }   // Danh sách sản phẩm trong đơn hàng

        /// <summary>
        /// Khởi tạo một đối tượng Order mới.
        /// </summary>
        /// <param name="id">Mã đơn hàng.</param>
        /// <param name="customerId">Thông tin khách hàng.</param>
        /// <param name="orderDate">Ngày đặt hàng.</param>
        /// <param name="items">Danh sách sản phẩm trong đơn hàng.</param>
        public Order(int id, int customerId, DateTime orderDate, LinkedList<OrderItem> items)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            Items = items;
        }

        /// <summary>
        /// Tính tổng giá trị của đơn hàng.
        /// </summary>
        /// <returns>Tổng giá trị của đơn hàng.</returns>
        public double Total()
        {
            double totalPrice = 0;
            foreach (OrderItem item in Items.ToList())
            {
                totalPrice += item.TotalPrice();
            }
            return totalPrice;
        }

        /// <summary>
        /// Chuyển đổi đối tượng Order thành một chuỗi.
        /// </summary>
        /// <returns>Chuỗi biểu diễn thông tin của đơn hàng.</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Customer ID: {CustomerId}, OrderDate: {OrderDate}, Items: {string.Join(", ", Items)}";
        }
    }

    /// <summary>
    /// Đại diện cho một mặt hàng trong đơn hàng.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Sản phẩm.
        /// </summary>
        public int ProductId { get; set; } // Sản phẩm

        /// <summary>
        /// Số lượng.
        /// </summary>
        public int Quantity { get; set; }    // Số lượng

        /// <summary>
        /// Giá của một đơn vị sản phẩm.
        /// </summary>
        public double UnitPrice { get; set; } // Giá sản phẩm

        /// <summary>
        /// Khởi tạo một đối tượng OrderItem mới.
        /// </summary>
        /// <param name="productId">Mã sản phẩm.</param>
        /// <param name="quantity">Số lượng sản phẩm.</param>
        /// <param name="unitPrice">Giá của một đơn vị sản phẩm.</param>
        public OrderItem(int productId, int quantity, double unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        /// <summary>
        /// Tính tổng giá trị của mặt hàng.
        /// </summary>
        /// <returns>Tổng giá trị của mặt hàng.</returns>
        public double TotalPrice()
        {
            return Quantity * UnitPrice;
        }

        /// <summary>
        /// Chuyển đổi đối tượng OrderItem thành một chuỗi.
        /// </summary>
        /// <returns>Chuỗi biểu diễn thông tin của mặt hàng.</returns>
        public override string ToString()
        {
            return $"ProductId: {ProductId}, Số lượng: {Quantity}, Giá:${UnitPrice}";
        }
    }
}
