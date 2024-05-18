using System;
using CafeManagement.Constants;
using CafeManagement.Helpers;
using CafeManagement.Models;
using CafeManagement.Services;


namespace CafeManagement.Manager
{
    public class CustomerManager
    {
        private CustomerService _customerService;
        private OrderService _orderService;

        public CustomerManager()
        {
            _customerService = new CustomerService("Data/CustomerData.txt");
            _orderService = new OrderService("Data/OrderData.txt");
        }

        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.CUSTOMER);
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayAllItems();
                        break;
                    case "2":
                        Add();
                        break;
                    case "3":
                        Update();
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

                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU);
                Console.ReadLine();
            }
        }

        private void DisplayAllItems()
        {
            LinkedList<Customer> customers = _customerService.GetAllItems();
            foreach (Customer customer in customers.ToList())
            {
                Console.WriteLine(customer);
            }
        }

        public void Add()
        {
            Console.Write("Nhập thông tin khách hàng mới:");
            string name = ConsoleHelper.GetStringInput("\tTên: ");
            DateTime birthday = ConsoleHelper.GetDateTimeInput("\tNgày sinh: ");
            string phoneNumber = ConsoleHelper.GetStringInput("\tSố điện thoại: ");
            string email = ConsoleHelper.GetStringInput("\tEmail: ");

            _customerService.Add(new Customer(name, birthday, phoneNumber, email));
            Console.WriteLine("Khách hàng đã được thêm thành công.");
        }

        public void Update()
        {
            int customerId = ConsoleHelper.GetIntInput("Nhập ID của khách hàng cần cập nhật: ");

            Customer customer = _customerService.GetById(customerId);
            if (customer != null)
            {
                Console.WriteLine($"Thông tin khách hàng cần cập nhật:");
                Console.WriteLine($"\tTên: {customer.Birthday}");
                Console.WriteLine($"\tNgày sinh: {customer.Birthday}");
                Console.WriteLine($"\tSố diện thoại: {customer.PhoneNumber}");
                Console.WriteLine($"\tEmail: {customer.Name}");

                while (true)
                {
                    ConsoleHelper.PrintTitleMenu("Lựa chọn thông tin cần cập nhật: ", false);
                    Console.WriteLine("1. Tên ");
                    Console.WriteLine("2. Ngày sinh");
                    Console.WriteLine("3. Số diện thoại");
                    Console.WriteLine("4. Email");
                    Console.WriteLine(StringConstants.BACK);
                    Console.Write(StringConstants.CHOOSE_AN_OPTION);

                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            string newName = ConsoleHelper.GetStringInput("Nhập tên khách hàng mới: ");
                            customer.Name = newName;
                            break;
                        case "2":
                            DateTime birthday = ConsoleHelper.GetDateTimeInput("Nhập ngày sinh mới: ");
                            customer.Birthday = birthday;
                            break;
                        case "3":
                            string phoneNumber = ConsoleHelper.GetStringInput("Nhập số điện thoại mới: ");
                            customer.PhoneNumber = phoneNumber;
                            break;
                        case "4":
                            string email = ConsoleHelper.GetStringInput("Nhập email mới: ");
                            customer.Email = email;
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy khách hàng với ID đã nhập.");
            }
        }

        public void Delete()
        {
            DisplayAllItems();
            int customerId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.CUSTOMER));
            if (!CanDeleteCustomer(customerId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.CUSTOMER), StringConstants.ORDER);
                return;
            }
            _customerService.Delete(customerId);
            Console.WriteLine("Khách hàng đã được xóa.");
        }

        public bool CanDeleteCustomer(int Id)
        {
            LinkedList<Order> orders = _orderService.GetAllItems();
            Node<Order> order = orders.Find(p => p.CustomerId == Id);
            if (orders != null)
            {
                return false;
            }
            return true;
        }
    }
}
