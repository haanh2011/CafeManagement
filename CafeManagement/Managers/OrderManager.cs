using System;
using System.Linq;
using CafeManagement.Models;
using CafeManagement.Services;
using CafeManagement.Utilities;
using CafeManagement.Constants;

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
                ConsoleHelper.PrintMenuDetails(StringConstants.ORDER);
                var choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        DisplayAllItems();
                        break;
                    case "2":
                        Add();
                        break;
                    case "3":
                        UpdateOrder();
                        break;
                    case "4":
                        Delete();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU);
                Console.ReadLine();
            }
        }

        public void DisplayAllItems()
        {
            LinkedList<Order> orders = orderService.GetAllItems();
            if (orders.Count > 0)
            {
                ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.ORDER));
                foreach (Order order in orders.ToList())
                {
                    DisplayOrder(order);
                }
            }
            else
            {
                string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.ORDER);
            }
        }

        public void DisplayOrder(Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"Mã đơn hàng: {order.Id}");
            Console.WriteLine($"Mã khách hàng: {order.CustomerId}");
            Console.WriteLine($"Ngày đặt hàng: {order.OrderDate.ToString(StringConstants.FORMAT_DATETIME)}");
            Console.WriteLine("Sản phẩm trong đơn hàng:");
            ConsoleHelper.PrintHeaderTable(StringConstants.ORDER);

            int i = 0;
            foreach (var item in order.Items.ToList())
            {
                i++;
                Product product = productService.GetById(item.ProductId);
                if (product != null)
                {
                    Console.WriteLine($"| {i,5} {product.ToString()} {item.Quantity,10} | {FormatHelper.FormatToVND(item.TotalPrice()),15} |");
                }
            }
            ConsoleHelper.PrintHorizontalLineOfTable(StringConstants.ORDER);

            Console.WriteLine($"Tổng tiền : {FormatHelper.FormatToVND(order.Total())}");
        }

        public void DisplayOrderById(int orderId)
        {
            Order order = orderService.GetById(orderId);
            if (order != null)
            {
                DisplayOrder(order);
            }
            else
            {
                Console.WriteLine("Đơn đặt hàng không tồn tại!");
            }
        }

        public void Add()
        {
            string customerPhoneNumber = ConsoleHelper.GetStringInput("Nhập số diện thoại khách hàng: ");
            // Tìm kiếm thông tin khách hàng dựa trên số điện thoại
            Customer customer = customerService.GetByPhoneNumber(customerPhoneNumber);

            if (customer == null)
            {
                Console.WriteLine("Không tìm thấy thông tin khách hàng với số điện thoại này.");
                Console.WriteLine("Bạn có muốn tạo mới thông tin khách hàng không? (Y/N)");

                string createNewCustomer = Console.ReadLine();
                if (createNewCustomer.ToUpper() == "Y")
                {
                    // Nhập thông tin mới của khách hàng
                    Console.WriteLine("Nhập thông tin khách hàng:");
                    string name = ConsoleHelper.GetStringInput("\tTên: ");
                    DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{StringConstants.BIRTHDAY} ({StringConstants.FORMAT_DATE}):");
                    string email = ConsoleHelper.GetStringInput("\tEmail: ");

                    // Tạo mới khách hàng
                    customer = new Customer(name, birthday, customerPhoneNumber, email);
                    customer = customerService.Add(customer);
                    Console.WriteLine("Khách hàng mới đã được tạo thành công.");
                }
                else
                {
                    return; // Quay lại menu chính nếu không muốn tạo mới
                }
            }

            DateTime orderDate = DateTime.Now;
            LinkedList<OrderItem> items = new LinkedList<OrderItem>();
            productService.GetAllItems();
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

                items.AddLast(new OrderItem(productId, quantity, product.Price));
            }

            Order order = new Order(0, customer.Id, orderDate, items);
            orderService.Add(order);
            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems());
            if (customer.Points > 0)
            {
                string keySubtractionPoints = ConsoleHelper.GetStringInput("Bạn có muốn sử dụng điểm tích luỹ để giảm giá tiền cho đơn hàng này không? (Y/N) ");
                if (keySubtractionPoints.ToUpper() == "Y")
                {
                    // Nhập thông tin mới của khách hàng
                    Console.WriteLine($"\tLưu ý bạn chỉ có {customer.Points} điểm!");
                    Console.WriteLine("\tNếu bạn nhập quá số điểm này chúng tôi sẽ cấn trừ theo giá tiền của đơn hàng đến khi hết điểm");
                    int points = ConsoleHelper.GetIntInput($"\tNhập số điểm bạn muốn cấn trừ (tối đa {customer.Points} điểm): ");
                    customer.SubtractionPoints(points);
                    customerService.Update(customer);
                    order.Points = customer.Points > points? points: customer.Points;
                    orderService.Update(order);
                }
            }
        }

        public void UpdateOrder()
        {
            int orderId = ConsoleHelper.GetIntInput("Nhập ID đơn hàng cần cập nhật: ");

            Order order = orderService.GetById(orderId);

            if (order != null)
            {
                Console.WriteLine($"Thông tin đơn hàng có mã {orderId}:");
                DisplayOrderById(orderId);

                while (true)
                {
                    Console.WriteLine("Chọn thành phần cần cập nhật:");
                    Console.WriteLine("1. " + string.Format(StringConstants.LIST_X, StringConstants.PRODUCT));
                    Console.WriteLine("2. Ngày đặt hàng");
                    Console.WriteLine("3. " + StringConstants.CUSTOMER);
                    Console.WriteLine("0. "+StringConstants.BACK);

                    int choice;
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            // Cập nhật danh sách sản phẩm
                            UpdateItems(order);
                            break;
                        case 2:
                            // Cập nhật ngày đặt hàng
                            order.OrderDate = DateTime.Now;
                            orderService.Update(order);
                            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
                            break;
                        case 3:
                            // Cập nhật thông tin khách hàng
                            UpdateCustomer(order);
                            break;
                        case 0:
                            return; // Quay lại menu chính
                        default:
                            Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_NOT_FOUND, StringConstants.ORDER));
            }
        }

        public void UpdateItems(Order order)
        {
            Console.WriteLine("Danh sách sản phẩm trong đơn hàng:");
            for (int i = 0; i < order.Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {order.Items.DataElementOfIndex(i)}");
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
                Node<OrderItem> selectedItem = order.Items.FindNodeAtIndex(choice - 1);

                // Hiển thị menu cập nhật sản phẩm
                Console.WriteLine($"Chọn thông tin cần cập nhật cho sản phẩm {selectedItem.Data.ProductId}:");
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
                        selectedItem.Data.Quantity = newQuantity;
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
                        selectedItem.Data.UnitPrice = newPrice;
                        break;
                    default:
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                        continue;
                }

                Console.WriteLine("Sản phẩm đã được cập nhật.");
                DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems()); // Lưu thay đổi vào file
            }
        }
        /// <summary>
        /// Cập nhật thông tin khách hàng cho đơn hàng.
        /// </summary>
        /// <param name="order">Đơn hàng cần cập nhật thông tin khách hàng.</param>
        public void UpdateCustomer(Order order)
        {
            Console.WriteLine("Nhập số điện thoại khách hàng:");
            string phoneNumber = Console.ReadLine();

            // Tìm kiếm thông tin khách hàng dựa trên số điện thoại
            Customer customer = customerService.GetByPhoneNumber(phoneNumber);

            if (customer == null)
            {
                Console.WriteLine("Không tìm thấy thông tin khách hàng với số điện thoại này.");
                Console.WriteLine("Bạn có muốn tạo mới thông tin khách hàng không? (Y/N)");

                string createNewCustomer = Console.ReadLine();
                if (createNewCustomer.ToUpper() == "Y")
                {
                    // Nhập thông tin mới của khách hàng
                    Console.WriteLine("Nhập thông tin khách hàng:");
                    string name = ConsoleHelper.GetStringInput("\tTên: ");
                    DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{StringConstants.BIRTHDAY} ({StringConstants.FORMAT_DATE}):");
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
            Console.WriteLine("Thông tin đơn hàng đã được cập nhật.");
        }

        /// <summary>
        /// Xóa đơn hàng.
        /// </summary>
        public void Delete()
        {
            // Hiển thị danh sách đơn hàng
            DisplayAllItems();
            // Nhập ID của đơn hàng cần xóa
            int orderId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.ORDER));
            // Kiểm tra xem có thể xóa đơn hàng không
            if (!CanDeleteOrder(orderId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.ORDER, StringConstants.INVOICE));
                return;
            }
            // Xóa đơn hàng từ dịch vụ đơn hàng
            orderService.Delete(orderId);
            // Lưu thay đổi vào file
            DataManager.SaveOrders("Data/OrderData.txt", orderService.GetAllItems());
        }


        /// <summary>
        /// Kiểm tra xem có thể xóa đơn hàng không dựa trên ID khách hàng.
        /// </summary>
        /// <param name="customerId">ID của khách hàng.</param>
        /// <returns>Trả về true nếu không có đơn hàng nào liên kết với khách hàng cung cấp, ngược lại trả về false.</returns>
        public bool CanDeleteOrder(int customerId)
        {
            // Lấy tất cả các đơn hàng từ dịch vụ đơn hàng
            LinkedList<Order> orders = orderService.GetAllItems();
            // Kiểm tra xem có bất kỳ đơn hàng nào liên kết với ID khách hàng cung cấp không
            Node<Order> order = orders.Find(p => p.CustomerId == customerId);
            // Trả về true nếu không tìm thấy bất kỳ đơn hàng nào liên kết với ID khách hàng, cho biết có thể xóa
            if (orders != null)
            {
                return false;
            }
            return true;
        }
    }
}
