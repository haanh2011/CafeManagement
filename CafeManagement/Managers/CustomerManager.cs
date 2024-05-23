using System;
using CafeManagement.Constants;
using CafeManagement.Utilities;
using CafeManagement.Models;
using CafeManagement.Services;


namespace CafeManagement.Manager
{
    /// <summary>
    /// Quản lý các hoạt động liên quan đến khách hàng.
    /// </summary>
    public class CustomerManager
    {
        private CustomerService _customerService; // Dịch vụ quản lý khách hàng
        private OrderService _orderService; // Dịch vụ quản lý đơn hàng
        private LinkedList<Order> _orders; // Danh sách lý đơn hàng
        private LinkedList<Customer> _customers; // Danh sách khách hàng

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp CustomerManager.
        /// </summary>
        public CustomerManager()
        {
            _customerService = new CustomerService("Data/CustomerData.txt"); // Khởi tạo dịch vụ quản lý khách hàng
            _orderService = new OrderService("Data/OrderData.txt"); // Khởi tạo dịch vụ quản lý đơn hàng
        }

        /// <summary>
        /// Hiển thị menu quản lý khách hàng và xử lý các tùy chọn người dùng.
        /// </summary>
        public void ShowMenu()
        {
            _orderService.GetAllItems();
            _customerService.GetAllItems();
            _orders = _orderService.Orders; // Danh sách lý đơn hàng
            _customers = _customerService.Customers; // Danh sách khách hàng
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.CUSTOMER); // Hiển thị tiêu đề menu
                var choice = Console.ReadLine(); // Nhập lựa chọn từ người dùng
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        DisplayAllItems(); // Hiển thị danh sách tất cả khách hàng
                        break;
                    case "2":
                        Add(); // Thêm khách hàng mới
                        break;
                    case "3":
                        Update(); // Cập nhật thông tin khách hàng
                        break;
                    case "4":
                        Delete(); // Xóa khách hàng
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION); // Thông báo lựa chọn không hợp lệ
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU); // Nhấn Enter để trở lại menu
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Hiển thị tất cả các khách hàng trong danh sách.
        /// </summary>
        private void DisplayAllItems()
        {
            if (_customers.Count > 0)
            {
                ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.CUSTOMER)); // Hiển thị tiêu đề danh sách
                ConsoleHelper.PrintHeaderTable(StringConstants.CUSTOMER);
                foreach (Customer customer in _customers.ToList())
                {
                    Console.WriteLine(customer.ToString()); // Hiển thị thông tin của từng khách hàng
                }
                ConsoleHelper.PrintHorizontalLineOfTable(StringConstants.CUSTOMER);
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.CUSTOMER)); // Thông báo nếu không có khách hàng nào
            }
        }

        /// <summary>
        /// Thêm một khách hàng mới.
        /// </summary>
        public void Add()
        {
            Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_ADD, StringConstants.CUSTOMER)); // Yêu cầu nhập thông tin khách hàng
            string name = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: "); // Nhập tên khách hàng
            DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{FormatHelper.ToTitleCase(StringConstants.BIRTHDAY)} ({StringConstants.FORMAT_DATE}):"); // Nhập ngày sinh

            string phoneNumber = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.PHONENUMBER)}: "); // Nhập số điện thoại
            string email = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.EMAIL)}: "); // Nhập địa chỉ email

            _customerService.Add(new Customer(name, birthday, email, phoneNumber)); // Thêm khách hàng mới
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.CUSTOMER)); // Thông báo thành công
        }


        /// <summary>
        /// Hiển thị menu và thực hiện chức năng quản lý danh sách khách hàng.
        /// </summary>
        public void Update()
        {
            DisplayAllItems(); // Hiển thị danh sách khách hàng
            Console.WriteLine();
            int customerId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_UPDATE, StringConstants.CUSTOMER)); // Nhập mã số khách hàng cần cập nhật
            Customer customer = _customerService.GetById(customerId); // Tìm khách hàng theo mã số
            if (customer == null)
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.CUSTOMER)); // Thông báo nếu không tìm thấy khách hàng
                string createNewCustomer = ConsoleHelper.GetStringInput("Không tồn tại khách hàng trong danh sách! Bạn có muốn tạo mới khách hàng không? (Y/N)"); // Hỏi người dùng có muốn tạo mới khách hàng không
                if (createNewCustomer.ToUpper() == "Y")
                {
                    Add(); // Thêm khách hàng mới nếu người dùng chọn 'Y'
                }
            }
            else
            {
                // Hiển thị thông tin khách hàng cần cập nhật
                Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_UPDATE, StringConstants.CUSTOMER));
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: {customer.Name}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.BIRTHDAY)}: {customer.Birthday}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.PHONENUMBER)}: {customer.PhoneNumber}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.EMAIL)}: {customer.Email}");

                while (true)
                {
                    Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_UPDATE, StringConstants.CUSTOMER));
                    ConsoleHelper.PrintTitleMenu(StringConstants.SELECT_INFORMATION_TO_UPDATE, false); // Hiển thị menu lựa chọn thông tin cần cập nhật
                    Console.WriteLine("1. " + FormatHelper.ToTitleCase(StringConstants.NAME));
                    Console.WriteLine("2. " + FormatHelper.ToTitleCase(StringConstants.BIRTHDAY));
                    Console.WriteLine("3. " + FormatHelper.ToTitleCase(StringConstants.PHONENUMBER));
                    Console.WriteLine("4. " + FormatHelper.ToTitleCase(StringConstants.EMAIL));
                    Console.WriteLine(StringConstants.BACK);
                    Console.Write(StringConstants.CHOOSE_AN_OPTION);

                    var choice = Console.ReadLine();
                    Console.WriteLine();
                    switch (choice)
                    {
                        case "1":
                            string newName = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CUSTOMER));
                            customer.Name = newName; // Cập nhật tên khách hàng
                            break;
                        case "2":
                            DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.BIRTHDAY)} ({StringConstants.FORMAT_DATE}):");
                            customer.Birthday = birthday; // Cập nhật ngày sinh của khách hàng
                            break;
                        case "3":
                            string phoneNumber = ConsoleHelper.GetStringInput("\t" + string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.PHONENUMBER));
                            customer.PhoneNumber = phoneNumber; // Cập nhật số điện thoại của khách hàng
                            break;
                        case "4":
                            string email = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.EMAIL));
                            customer.Email = email; // Cập nhật địa chỉ email của khách hàng
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Xóa một khách hàng khỏi danh sách nếu không có đơn hàng nào liên kết với khách hàng đó.
        /// </summary>
        public void Delete()
        {
            DisplayAllItems(); // Hiển thị danh sách khách hàng
            int customerId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.CUSTOMER)); // Nhập mã số khách hàng cần xóa
            if (!CanDeleteCustomer(customerId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.CUSTOMER, StringConstants.ORDER)); // Thông báo không thể xóa nếu có đơn hàng liên kết với khách hàng
                return;
            }
            _customerService.Delete(customerId); // Xóa khách hàng nếu không có đơn hàng liên kết
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_DELETE, StringConstants.CUSTOMER)); // Thông báo xóa thành công
        }

        /// <summary>
        /// Kiểm tra xem một khách hàng có thể bị xóa không dựa trên việc có đơn hàng liên kết với khách hàng đó hay không.
        /// </summary>
        /// <param name="Id">Mã số của khách hàng cần kiểm tra.</param>
        /// <returns>Trả về true nếu không có đơn hàng nào liên kết với khách hàng, ngược lại trả về false.</returns>
        public bool CanDeleteCustomer(int Id)
        {
            _orders = _orderService.Orders; // Lấy danh sách tất cả các đơn hàng
            Node<Order> order = _orders.Find(p => p.CustomerId == Id); // Tìm đơn hàng có mã số khách hàng trùng khớp với Id
            if (_orders != null)
            {
                return false; // Trả về false nếu có đơn hàng liên kết với khách hàng
            }
            return true; // Trả về true nếu không có đơn hàng nào liên kết với khách hàng
        }
    }
}
