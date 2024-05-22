using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CafeManagement.Constants;
using CafeManagement.Models;
using CafeManagement.Utilities;
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
        public static Utilities.LinkedList<Category> LoadCategories(string filePath)
        {
            // Mảng danh sách các danh mục
            Utilities.LinkedList<Category> categories = new Utilities.LinkedList<Category>();

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
        public static void SaveCategories(string filePath, Utilities.LinkedList<Category> categories)
        {
            // Mảng danh sách các dòng để lưu vào tệp
            Utilities.LinkedList<string> lines = new Utilities.LinkedList<string>();

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
        public static Utilities.LinkedList<Product> LoadProducts(string filePath)
        {
            Utilities.LinkedList<Product> products = new Utilities.LinkedList<Product>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                int i = 0;
                foreach (var line in lines)
                {
                    i++;
                    var parts = line.Split(',');
                    if (parts.Length == 4 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[2], out int categoryId) &&
                        double.TryParse(parts[3], out double price))
                    {
                        products.AddLast(new Product(parts[1], categoryId, price, i));
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
        /// <summary>
        /// Lưu danh sách các sản phẩm vào tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp để lưu dữ liệu sản phẩm.</param>
        /// <param name="products">Danh sách các sản phẩm cần lưu.</param>
        public static void SaveProducts(string filePath, Utilities.LinkedList<Product> products)
        {
            // Sắp xếp danh sách sản phẩm theo CategoryId và tên sản phẩm
            var sortedProducts = products.ToList()
                .OrderBy(p => p.CategoryId)
                .ThenBy(p => p.Name)
                .ToList();

            // Cập nhật ID của sản phẩm theo thứ tự tăng dần
            for (int i = 0; i < sortedProducts.Count; i++)
            {
                sortedProducts[i].Id = i + 1;
            }

            // Chuyển đổi danh sách sản phẩm thành danh sách chuỗi để lưu vào tệp
            var lines = new List<string>();
            foreach (var product in sortedProducts)
            {
                lines.Add($"{product.Id},{product.Name},{product.CategoryId},{product.Price}");
            }

            // Ghi các dòng vào tệp
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Tải danh sách các khách hàng từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu khách hàng.</param>
        /// <returns>Danh sách các khách hàng được tải.</returns>
        public static Utilities.LinkedList<Customer> LoadCustomers(string filePath)
        {
            Utilities.LinkedList<Customer> customers = new Utilities.LinkedList<Customer>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 6 && int.TryParse(parts[0], out int id))
                    {
                        int points = int.TryParse(parts[5], out int pointVal) ? pointVal : 0;
                        DateTime birth = DateTime.MinValue;
                        try
                        {

                            // Chuyển đổi chuỗi đầu vào thành đối tượng DateTime
                            birth = DateTime.ParseExact(parts[2], StringConstants.FORMAT_DATE, CultureInfo.InvariantCulture);
                            customers.AddLast(new Customer(parts[1], birth, parts[3], parts[4], id, points));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng kiểm tra file 'Data/CustomerData.txt'.");
                        }
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
        public static void SaveCustomers(string filePath, Utilities.LinkedList<Customer> customers)
        {
            var lines = new Utilities.LinkedList<string>();
            foreach (Customer customer in customers.ToList())
            {
                lines.AddLast($"{customer.Id},{customer.Name},{customer.Birthday.ToString(StringConstants.FORMAT_DATE)},{customer.Email},{customer.PhoneNumber},{customer.Points}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

        /// <summary>
        /// Tải danh sách các đơn đặt hàng từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu đơn đặt hàng.</param>
        /// <returns>Danh sách các đơn đặt hàng được tải.</returns>
        public static Utilities.LinkedList<Order> LoadOrders(string filePath)
        {
            Utilities.LinkedList<Order> orders = new Utilities.LinkedList<Order>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 5 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[1], out int customerId) &&
                        DateTime.TryParse(parts[2], out DateTime orderDate)
                        && double.TryParse(parts[4], out double points))
                    {
                        Utilities.LinkedList<OrderItem> items = new Utilities.LinkedList<OrderItem>();
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
                        orders.AddLast(new Order(id, customerId, orderDate, items, points));
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
        public static void SaveOrders(string filePath, Utilities.LinkedList<Order> orders)
        {
            Utilities.LinkedList<string> lines = new Utilities.LinkedList<string>();
            foreach (Order order in orders.ToList())
            {
                var items = string.Join(",", order.Items.ToList().Select(i => $"{i.ProductId}:{i.Quantity}:{i.UnitPrice}"));
                lines.AddLast($"{order.Id}|{order.CustomerId}|{order.OrderDate:yyyy-MM-dd}|{items}|{order.Points}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

        /// <summary>
        /// Tải danh sách các hóa đơn từ tệp.
        /// </summary>
        /// <param name="filePath">Đường dẫn tới tệp chứa dữ liệu hóa đơn.</param>
        /// <returns>Danh sách các hóa đơn được tải.</returns>
        public static Utilities.LinkedList<Invoice> LoadInvoices(string filePath)
        {
            Utilities.LinkedList<Invoice> invoices = new Utilities.LinkedList<Invoice>();
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
        public static void SaveInvoices(string filePath, Utilities.LinkedList<Invoice> invoices)
        {
            Utilities.LinkedList<string> lines = new Utilities.LinkedList<string>();
            foreach (Invoice invoice in invoices.ToList())
            {
                lines.AddLast($"{invoice.Id}|{invoice.OrderId}|");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }
    }
}
