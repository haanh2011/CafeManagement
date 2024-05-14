using System;
using System.Collections.Generic;
using CafeManagement.Models;
using CafeManagement.Services;
using CafeManagement.Helpers;
using System.Text;
using CafeManagement.Data;

namespace CafeManagement
{
    class Program
    {
        static string categoryFilePath = "Data/CategoryData.txt";
        static string productFilePath = "Data/ProductData.txt";
        static string orderFilePath = "Data/OrderData.txt";
        static string invoiceFilePath = "Data/InvoiceData.txt";
        static string customerFilePath = "Data/CustomerData.txt";

        // Đọc dữ liệu từ các file txt
        static List<Category> categories = DataManager.LoadCategories(categoryFilePath);
        static List<Product> products = DataManager.LoadProducts(productFilePath);
        static List<Customer> customers = DataManager.LoadCustomers(customerFilePath);
        static List<Order> orders = DataManager.LoadOrders(orderFilePath);
        static List<Invoice> invoices = DataManager.LoadInvoices(invoiceFilePath);

        // Tạo các đối tượng dịch vụ
        static CategoryService categoryService = new CategoryService(categoryFilePath, categories);
        static ProductService productService = new ProductService(productFilePath, products);
        static CustomerService customerService = new CustomerService(customerFilePath, customers);
        static OrderService orderService = new OrderService(orderFilePath, orders);
        static InvoiceService invoiceService = new InvoiceService(invoiceFilePath, invoices);
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            while (true)
            {
                Console.WriteLine("===== Quản Lý Quán Cà Phê =====");
                Console.WriteLine("1. Quản lý sản phẩm");
                Console.WriteLine("2. Quản lý đơn hàng");
                Console.WriteLine("3. Quản lý hóa đơn");
                Console.WriteLine("4. Quản lý khách hàng");
                Console.WriteLine("0. Thoát");

                int choice = ConsoleHelper.GetIntInput("Nhập lựa chọn của bạn: ");

                switch (choice)
                {
                    case 1:
                        ManageProducts(productService, categoryService);
                        break;
                    case 2:
                        ManageOrders(orderService);
                        break;
                    case 3:
                        //ManageInvoices(invoiceService, orderService);
                        break;
                    case 4:
                        ManageCustomers(customerService);
                        break;
                    case 0:
                        Console.WriteLine("Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        static void ManageProducts(ProductService productService, CategoryService categoryService)
        {
            while (true)
            {
                Console.WriteLine("===== Quản Lý Sản Phẩm =====");
                Console.WriteLine("1. Hiển thị danh sách sản phẩm");
                Console.WriteLine("2. Thêm sản phẩm");
                Console.WriteLine("3. Cập nhật sản phẩm");
                Console.WriteLine("4. Xóa sản phẩm");
                Console.WriteLine("0. Quay lại");

                int choice = ConsoleHelper.GetIntInput("Nhập lựa chọn của bạn: ");

                switch (choice)
                {
                    case 1:
                        ShowProducts();
                        break;
                    case 2:
                        AddProduct(productService, categoryService);
                        break;
                    case 3:
                        UpdateProduct(productService);
                        break;
                    case 4:
                        DeleteProduct(productService);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        static void ShowProducts()
        {
            Console.WriteLine("===== Danh Sách Sản Phẩm =====");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}. {product.Name} - {product.Price}");
            }
        }

        static void AddProduct(ProductService productService, CategoryService categoryService)
        {
            Console.WriteLine("===== Thêm Sản Phẩm Mới =====");
            string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm: ");
            decimal price = ConsoleHelper.GetDecimalInput("Nhập giá sản phẩm: ");

            Console.WriteLine("Danh sách các loại sản phẩm có sẵn:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }
            int categoryId = ConsoleHelper.GetIntInput("Chọn loại sản phẩm: ");

            var product = new Product(name, categoryId, price);
            productService.AddProduct(product);
            Console.WriteLine("Đã thêm sản phẩm thành công!");
        }

        static void UpdateProduct(ProductService productService)
        {
            Console.WriteLine("===== Cập Nhật Sản Phẩm =====");
            ShowProducts();
            int productId = ConsoleHelper.GetIntInput("Nhập ID sản phẩm cần cập nhật: ");

            var product = productService.GetProductById(productId);
            if (product != null)
            {
                string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm mới: ");
                decimal price = ConsoleHelper.GetDecimalInput("Nhập giá sản phẩm mới: ");

                product.Name = name;
                product.Price = price;

                productService.UpdateProduct(product);
                Console.WriteLine("Đã cập nhật sản phẩm thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm!");
            }
        }

        static void DeleteProduct(ProductService productService)
        {
            Console.WriteLine("===== Xóa Sản Phẩm =====");
            ShowProducts();
            int productId = ConsoleHelper.GetIntInput("Nhập ID sản phẩm cần xóa: ");

            var product = productService.GetProductById(productId);
            if (product != null)
            {
                productService.DeleteProduct(productId);
                Console.WriteLine("Đã xóa sản phẩm thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm!");
            }
        }

        static void ManageOrders(OrderService orderService)
        {
            // Implement the logic for managing orders
        }

        static void ManageInvoices(InvoiceService invoiceService)
        {
            // Implement the logic for managing invoices
        }
        static void ManageCustomers(CustomerService customerService)
        {
            // Implement the logic for managing customers
        }
    }
}
