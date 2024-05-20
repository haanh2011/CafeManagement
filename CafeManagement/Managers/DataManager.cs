using System;
using System.IO;
using System.Linq;
using CafeManagement.Models;
namespace CafeManagement.Manager
{
    /// <summary>
    /// Quản lý việc tải và lưu dữ liệu cho các loại đối tượng.
    /// </summary>
    public static class DataManager
    {
        /// <summary>
        /// Tải danh sách các danh mục từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu danh mục.</param>
        /// <returns>Danh sách các danh mục được tải.</returns>
        public static LinkedList<Category> LoadCategories(string filePath)
        {
            // Mảng danh sách các danh mục
            LinkedList<Category> categories = new LinkedList<Category>();

            // Kiểm tra nếu tệp tồn tại
            if (File.Exists(filePath))
            {
                // Đọc tất cả các dòng từ tệp
                var lines = File.ReadAllLines(filePath);

                // Duyệt qua từng dòng để tạo các đối tượng danh mục
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int id))
                    {
                        categories.AddLast(new Category(parts[1], id));
                    }
                }
            }
            return categories;
        }

        /// <summary>
        /// Lưu danh sách các danh mục vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu danh mục.</param>
        /// <param name="categories">Danh sách các danh mục cần lưu.</param>
        public static void SaveCategories(string filePath, LinkedList<Category> categories)
        {
            // Mảng danh sách các dòng để lưu vào tệp
            LinkedList<string> lines = new LinkedList<string>();

            // Duyệt qua từng danh mục và tạo dòng dữ liệu tương ứng
            foreach (Category category in categories.ToList())
            {
                lines.AddLast($"{category.Id},{category.Name}");
            }

            // Ghi các dòng vào tệp
            File.WriteAllLines(filePath, lines.ToList());
        }
        /// <summary>
        /// Tải danh sách các sản phẩm từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu sản phẩm.</param>
        /// <returns>Danh sách các sản phẩm được tải.</returns>
        public static LinkedList<Product> LoadProducts(string filePath)
        {
            LinkedList<Product> products = new LinkedList<Product>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[2], out int categoryId) &&
                        double.TryParse(parts[3], out double price))
                    {
                        products.AddLast(new Product(parts[1], categoryId, price, id));
                    }
                }
            }
            return products;
        }

        /// <summary>
        /// Lưu danh sách các sản phẩm vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu sản phẩm.</param>
        /// <param name="products">Danh sách các sản phẩm cần lưu.</param>
        public static void SaveProducts(string filePath, LinkedList<Product> products)
        {
            var lines = new LinkedList<string>();
            foreach (var product in products.ToList())
            {
                lines.AddLast($"{product.Id},{product.Name},{product.CategoryId},{product.Price}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

        /// <summary>
        /// Tải danh sách các khách hàng từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu khách hàng.</param>
        /// <returns>Danh sách các khách hàng được tải.</returns>
        public static LinkedList<Customer> LoadCustomers(string filePath)
        {
            LinkedList<Customer> customers = new LinkedList<Customer>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5 && int.TryParse(parts[0], out int id))
                    {
                        int points = int.TryParse(parts[4], out int pointVal) ? pointVal : 0;
                        DateTime birthday = DateTime.TryParse(parts[2], out DateTime birth) ? birth : DateTime.MinValue;
                        customers.AddLast(new Customer(parts[1], birthday, parts[3], parts[4], id, points));
                    }
                }
            }
            return customers;
        }

        /// <summary>
        /// Lưu danh sách các khách hàng vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu khách hàng.</param>
        /// <param name="customers">Danh sách các khách hàng cần lưu.</param>
        public static void SaveCustomers(string filePath, LinkedList<Customer> customers)
        {
            var lines = new LinkedList<string>();
            foreach (Customer customer in customers.ToList())
            {
                lines.AddLast($"{customer.Id},{customer.Name},{customer.Email},{customer.PhoneNumber},{customer.Points}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

        /// <summary>
        /// Tải danh sách các đơn đặt hàng từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu đơn đặt hàng.</param>
        /// <returns>Danh sách các đơn đặt hàng được tải.</returns>
        public static LinkedList<Order> LoadOrders(string filePath)
        {
            LinkedList<Order> orders = new LinkedList<Order>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 4 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[1], out int customerId) &&
                        DateTime.TryParse(parts[2], out DateTime orderDate))
                    {
                        LinkedList<OrderItem> items = new LinkedList<OrderItem>();
                        var itemsParts = parts[3].Split(',');
                        foreach (var itemPart in itemsParts)
                        {
                            var itemDetails = itemPart.Split(':');
                            if (itemDetails.Length == 3 &&
                                int.TryParse(itemDetails[0], out int productId) &&
                                int.TryParse(itemDetails[1], out int quantity) &&
                                double.TryParse(itemDetails[2], out double unitPrice))
                            {
                                items.AddLast(new OrderItem(productId, quantity, unitPrice));
                            }
                        }

                        orders.AddLast(new Order(id, customerId, orderDate, items));
                    }
                }
            }
            return orders;
        }

        /// <summary>
        /// Lưu danh sách các đơn đặt hàng vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu đơn đặt hàng.</param>
        /// <param name="orders">Danh sách các đơn đặt hàng cần lưu.</param>
        public static void SaveOrders(string filePath, LinkedList<Order> orders)
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (Order order in orders.ToList())
            {
                var items = string.Join(",", order.Items.ToList().Select(i => $"{i.ProductId}:{i.Quantity}:{i.UnitPrice}"));
                lines.AddLast($"{order.Id}|{order.CustomerId}|{order.OrderDate:yyyy-MM-dd}|{items}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

        /// <summary>
        /// Tải danh sách các hóa đơn từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu hóa đơn.</param>
        /// <returns>Danh sách các hóa đơn được tải.</returns>
        public static LinkedList<Invoice> LoadInvoices(string filePath)
        {
            LinkedList<Invoice> invoices = new LinkedList<Invoice>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[1], out int orderId) &&
                        DateTime.TryParse(parts[2], out DateTime date))
                    {
                        invoices.AddLast(new Invoice(id, orderId, date));
                    }
                }
            }
            return invoices;
        }

        /// <summary>
        /// Lưu danh sách các hóa đơn vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu hóa đơn.</param>
        /// <param name="invoices">Danh sách các hóa đơn cần lưu.</param>
        public static void SaveInvoices(string filePath, LinkedList<Invoice> invoices)
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (Invoice invoice in invoices.ToList())
            {
                lines.AddLast($"{invoice.Id}|{invoice.OrderId}|");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

    }
}
