using System;
using System.IO;
using System.Linq;
using CafeManagement.Models;
namespace CafeManagement.Manager
{
    public static class DataManager
    {
        public static LinkedList<Category> LoadCategories(string filePath)
        {
            LinkedList<Category> categories = new LinkedList<Category>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
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

        public static void SaveCategories(string filePath,LinkedList<Category> categories)
        {
            LinkedList<string> lines = new LinkedList<string>();
            foreach (Category category in categories.ToList())
            {
                lines.AddLast($"{category.Id},{category.Name}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

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

        public static void SaveProducts(string filePath, LinkedList<Product> products)
        {
            var lines = new LinkedList<string>();
            foreach (var product in products.ToList())
            {
                lines.AddLast($"{product.Id},{product.Name},{product.CategoryId},{product.Price}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

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

        public static void SaveCustomers(string filePath, LinkedList<Customer> customers)
        {
            var lines = new LinkedList<string>();
            foreach (Customer customer in customers.ToList())
            {
                lines.AddLast($"{customer.Id},{customer.Name},{customer.Email},{customer.PhoneNumber},{customer.Points}");
            }
            File.WriteAllLines(filePath, lines.ToList());
        }

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
