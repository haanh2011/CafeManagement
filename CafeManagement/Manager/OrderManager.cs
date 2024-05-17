using System;
using CafeManagement.Models;
using System.Collections.Generic;
using CafeManagement.Services;
using CafeManagement.Helpers;
using Microsoft.SqlServer.Server;

namespace CafeManagement.Manager
{
    public class OrderManager
    {
        public OrderService orderService;
        public ProductService productService;
        public CustomerService customerService;

        public OrderManager()
        {
            orderService = new OrderService("Data/OrderData.txt");
            productService = new ProductService("Data/ProductData.txt");
            customerService = new CustomerService("Data/CustomerData.txt");
        }

        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintTitleMenu("Quản Lý Đơn Hàng");
                Console.WriteLine("1. Hiển thị danh sách đơn hàng");
                Console.WriteLine("2. Thêm đơn hàng");
                Console.WriteLine("3. Cập nhật đơn hàng");
                Console.WriteLine("4. Xóa đơn hàng");
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn một tùy chọn: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayOrders();
                        break;
                    case "2":
                        AddOrder();
                        break;
                    case "3":
                        UpdateOrder();
                        break;
                    case "4":
                        DeleteOrder();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
                Console.ReadLine();
            }
        }

        public void DisplayOrders()
        {
            List<Order> orders = orderService.GetAllItems();
            if (orders.Count < 1)
            {
                Console.WriteLine("Không có đơn đặt hàng nào!");
            }
            foreach (var order in orders)
            {
                DisplayOrder(order);
            }
        }

        public void DisplayOrder(Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"Mã đơn hàng: {order.Id}");
            Console.WriteLine($"Mã khách hàng: {order.CustomerId}");
            Console.WriteLine($"Ngày đặt hàng: {order.OrderDate:yyyy-MM-dd}");
            Console.WriteLine("Sản phẩm trong đơn hàng:");
            Console.WriteLine("| {0} | {1,-55} | {2,10} | {3,10} | {4,10} |", "STT", "Tên sản phẩm", "Số lượng", "Giá", "Thành tiền");

            int i = 0;
            foreach (var item in order.Items)
            {
                i++;
                Product product = productService.GetById(item.ProductId);
                if (!product.Equals(default(Product)))
                    Console.WriteLine("| {0,3} | {1,-55} | {2,10} | {3,10} | {4,10} |", i, product.Name, item.Quantity, FormatHelper.FormatNumberEuropeanStyle(product.Price), FormatHelper.FormatNumberEuropeanStyle(item.TotalPrice()));
            }
        }

        public void DisplayOrderById(int orderId)
        {
            Order order = orderService.GetById(orderId);
            if (order.Equals(default(Order)))
            {
                Console.WriteLine("Đơn đặt hàng không tồn tại!");
                return;
            }
            DisplayOrder(order);
        }

        public void AddOrder()
        {
            string customerPhoneNumber = ConsoleHelper.GetStringInput("Nhập số diện thoại khách hàng: ");
            // Tìm kiếm thông tin khách hàng dựa trên số điện thoại
            Customer customer = customerService.GetByPhoneNumber(customerPhoneNumber);

            if (customer.Equals(default(Customer)))
            {
                Console.WriteLine("Không tìm thấy thông tin khách hàng với số điện thoại này.");
                Console.WriteLine("Bạn có muốn tạo mới thông tin khách hàng không? (Y/N)");

                string createNewCustomer = Console.ReadLine();
                if (createNewCustomer.ToUpper() == "Y")
                {
                    // Nhập thông tin mới của khách hàng
                    Console.WriteLine("Nhập thông tin khách hàng:");
                    string name = ConsoleHelper.GetStringInput("\tTên: ");
                    DateTime birthday = ConsoleHelper.GetDateTimeInput("\tNgày sinh: ");
                    string email = ConsoleHelper.GetStringInput("\tEmail: ");

                    // Tạo mới khách hàng
                    customer = new Customer(name, birthday, customerPhoneNumber, email);
                    customer.Id = customerService.Add(customer);
                    DataManager.SaveCustomers("Data/CustomerData.txt", customerService.GetAllItems());
                    Console.WriteLine("Khách hàng mới đã được tạo thành công.");
                }
                else
                {
                    return; // Quay lại menu chính nếu không muốn tạo mới
                }
            }
            DateTime orderDate = DateTime.Now;

            var items = new List<OrderItem>();
            while (true)
            {
                int input = ConsoleHelper.GetIntInput("Nhập mã sản phẩm (hoặc '0' để kết thúc): ");
                if (input == 0)
                {
                    break;
                }
                int productId = input;
                int quantity = ConsoleHelper.GetIntInput("Nhập số lượng: ");
                Product product = productService.GetById(productId);

                items.Add(new OrderItem(productId, quantity, product.Price));
            }

            var order = new Order(0, customer.Id, orderDate, items);
            orderService.Add(order);
            DataManager.SaveOrders("Data/OrderrData.txt", orderService.GetAllItems());
        }

        public void UpdateOrder()
        {
            int orderId = ConsoleHelper.GetIntInput("Nhập ID đơn hàng cần cập nhật: ");

            Order order = orderService.GetById(orderId);

            if (order.Equals(default(Order)))
            {
                Console.WriteLine("Không tìm thấy đơn hàng.");

            }
            else
            {
                Console.WriteLine($"Thông tin đơn hàng có mã {orderId}:");
                Console.WriteLine(order);

                while (true)
                {
                    Console.WriteLine("Chọn thành phần cần cập nhật:");
                    Console.WriteLine("1. Danh sách sản phẩm");
                    Console.WriteLine("2. Ngày đặt hàng");
                    Console.WriteLine("3. Khách hàng");
                    Console.WriteLine("0. Quay lại");

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Vui lòng nhập số từ menu.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            // Cập nhật danh sách sản phẩm  var items = new List<OrderItem>();
                            UpdateOrderItems(order);
                            break;
                        case 2:
                            // Cập nhật ngày đặt hàng
                            DateTime datetime = ConsoleHelper.GetDateTimeInput("Nhập thời gian đặt hàng: ");
                            order.OrderDate = datetime;
                            orderService.Update(order);
                            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
                            break;
                        case 3:
                            // Cập nhật thông tin khách hàng
                            UpdateOrderCustomer(order);
                            break;
                        case 0:
                            return; // Quay lại menu chính
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
            }
        }

        public void UpdateOrderItems(Order order)
        {
            Console.WriteLine("Danh sách sản phẩm trong đơn hàng:");
            for (int i = 0; i < order.Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {order.Items[i]}");
            }

            while (true)
            {
                Console.WriteLine("Chọn sản phẩm cần cập nhật (0 để thoát):");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Vui lòng nhập số từ danh sách.");
                    continue;
                }

                if (choice == 0)
                    return; // Thoát nếu người dùng chọn 0

                if (choice < 1 || choice > order.Items.Count)
                {
                    Console.WriteLine("Sản phẩm không tồn tại trong danh sách.");
                    continue;
                }

                // Lấy thông tin sản phẩm được chọn
                var selectedItem = order.Items[choice - 1];

                // Hiển thị menu cập nhật sản phẩm
                Console.WriteLine($"Chọn thông tin cần cập nhật cho sản phẩm {selectedItem.ProductId}:");
                Console.WriteLine("1. Số lượng");
                Console.WriteLine("2. Giá");

                int option;
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Vui lòng nhập số từ menu.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        // Cập nhật số lượng
                        Console.Write("Nhập số lượng mới: ");
                        int newQuantity;
                        if (!int.TryParse(Console.ReadLine(), out newQuantity) || newQuantity < 0)
                        {
                            Console.WriteLine("Số lượng không hợp lệ.");
                            continue;
                        }
                        selectedItem.Quantity = newQuantity;
                        break;
                    case 2:
                        // Cập nhật giá
                        Console.Write("Nhập giá mới: ");
                        double newPrice;
                        if (!double.TryParse(Console.ReadLine(), out newPrice) || newPrice < 0)
                        {
                            Console.WriteLine("Giá không hợp lệ.");
                            continue;
                        }
                        selectedItem.UnitPrice = newPrice;
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        continue;
                }

                Console.WriteLine("Sản phẩm đã được cập nhật.");
                DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
            }
        }

        public void UpdateOrderCustomer(Order order)
        {
            Console.WriteLine("Nhập số điện thoại khách hàng:");
            string phoneNumber = Console.ReadLine();

            // Tìm kiếm thông tin khách hàng dựa trên số điện thoại
            var customer = customerService.GetByPhoneNumber(phoneNumber);

            if (customer.Equals(default(Customer)))
            {
                Console.WriteLine("Không tìm thấy thông tin khách hàng với số điện thoại này.");
                Console.WriteLine("Bạn có muốn tạo mới thông tin khách hàng không? (Y/N)");

                string createNewCustomer = Console.ReadLine();
                if (createNewCustomer.ToUpper() == "Y")
                {
                    // Nhập thông tin mới của khách hàng
                    Console.WriteLine("Nhập thông tin khách hàng:");
                    string name = ConsoleHelper.GetStringInput("\tTên: ");
                    DateTime birthday = ConsoleHelper.GetDateTimeInput("\tNgày sinh: ");
                    string email = ConsoleHelper.GetStringInput("\tEmail: ");

                    // Tạo mới khách hàng
                    customer = new Customer(name, birthday, phoneNumber, email);
                    customerService.Add(customer);
                    DataManager.SaveCustomers("Data/CustomerData.txt", customerService.GetAllItems()); // Lưu thay đổi vào file
                    Console.WriteLine("Khách hàng mới đã được tạo thành công.");
                }
                else
                {
                    return; // Quay lại menu chính nếu không muốn tạo mới
                }
            }

            // Cập nhật thông tin khách hàng cho đơn hàng
            order.CustomerId = customer.Id;
            orderService.Update(order);
            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
            Console.WriteLine("Thông tin khách hàng đã được cập nhật.");
        }

        public void DeleteOrder()
        {
            Console.Write("Nhập mã đơn hàng cần xóa: ");
            int orderId = int.Parse(Console.ReadLine());
            orderService.Delete(orderId);
            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
        }
    }
}
