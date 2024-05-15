using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CafeManagement.Models;
namespace CafeManagement.Manager
{
    public static class DataManager
    {
        public static List<Category> LoadCategories(string filePath)
        {
            var categories = new List<Category>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int id))
                    {
                        categories.Add(new Category(parts[1], id));
                    }
                }
            }
            return categories;
        }

        public static void SaveCategories(string filePath, List<Category> categories)
        {
            var lines = new List<string>();
            foreach (var category in categories)
            {
                lines.Add($"{category.Id},{category.Name}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public static List<Product> LoadProducts(string filePath)
        {
            var products = new List<Product>();
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
                        products.Add(new Product(parts[1], categoryId, price, id));
                    }
                }
            }
            return products;
        }

        public static void SaveProducts(string filePath, List<Product> products)
        {
            var lines = new List<string>();
            foreach (var product in products)
            {
                lines.Add($"{product.Id},{product.Name},{product.CategoryId},{product.Price}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public static List<Customer> LoadCustomers(string filePath)
        {
            var customers = new List<Customer>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5 && int.TryParse(parts[0], out int id))
                    {
                        var points = int.TryParse(parts[4], out int pointVal) ? pointVal : 0;
                        customers.Add(new Customer(parts[1], parts[2], parts[3], id, points));
                    }
                }
            }
            return customers;
        }

        public static void SaveCustomers(string filePath, List<Customer> customers)
        {
            var lines = new List<string>();
            foreach (var customer in customers)
            {
                lines.Add($"{customer.Id},{customer.Name},{customer.Email},{customer.PhoneNumber},{customer.Points}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public static List<Order> LoadOrders(string filePath)
        {
            var orders = new List<Order>();
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
                        var items = new List<OrderItem>();
                        var itemsParts = parts[3].Split(',');
                        foreach (var itemPart in itemsParts)
                        {
                            var itemDetails = itemPart.Split(':');
                            if (itemDetails.Length == 3 &&
                                int.TryParse(itemDetails[0], out int productId) &&
                                int.TryParse(itemDetails[1], out int quantity) &&
                                double.TryParse(itemDetails[2], out double unitPrice))
                            {
                                items.Add(new OrderItem(productId, quantity, unitPrice));
                            }
                        }

                        orders.Add(new Order(id, customerId, orderDate, items));
                    }
                }
            }
            return orders;
        }

        public static void SaveOrders(string filePath, List<Order> orders)
        {
            var lines = new List<string>();
            foreach (var order in orders)
            {
                var items = string.Join(",", order.Items.Select(i => $"{i.ProductId}:{i.Quantity}:{i.UnitPrice}"));
                lines.Add($"{order.Id}|{order.CustomerId}|{order.OrderDate:yyyy-MM-dd}|{items}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public static List<Invoice> LoadInvoices(string filePath)
        {
            var invoices = new List<Invoice>();
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[1], out int orderId) &&
                        double.TryParse(parts[2], out double totalAmount) &&
                        DateTime.TryParse(parts[3], out DateTime date))
                    {
                        invoices.Add(new Invoice(id,  orderId, totalAmount, date));
                    }
                }
            }
            return invoices;
        }

        public static void SaveInvoices(string filePath, List<Invoice> invoices)
        {
            var lines = new List<string>();
            foreach (var invoice in invoices)
            {
                lines.Add($"{invoice.Id}|{invoice.OrderId}|{invoice.Total}");
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
