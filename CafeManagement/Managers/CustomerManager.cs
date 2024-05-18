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

                Console.WriteLine();
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU);
                Console.ReadLine();
            }
        }

        private void DisplayAllItems()
        {
            LinkedList<Customer> customers = _customerService.GetAllItems();
            if (customers.Count > 0)
            {
                ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.CUSTOMER));
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.CUSTOMER));
            }
            foreach (Customer customer in customers.ToList())
            {
                Console.WriteLine(customer.ToString());
            }
        }

        public void Add()
        {
            Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_ADD, StringConstants.CUSTOMER));
            string name = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: ");
            DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{FormatHelper.ToTitleCase(StringConstants.BIRTHDAY)}({StringConstants.FORMAT_DATE}):", StringConstants.FORMAT_DATE);
            string phoneNumber = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.PHONENUMBER)}: ");
            string email = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.EMAIL)}: ");

            _customerService.Add(new Customer(name, birthday, phoneNumber, email));
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.CUSTOMER));
        }

        public void Update()
        {
            DisplayAllItems();
            int customerId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_ADD, StringConstants.CUSTOMER));
            Customer customer = _customerService.GetById(customerId);
            if (customer != null)
            {
                Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_UPDATE, StringConstants.CUSTOMER));
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: {customer.Name}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.BIRTHDAY)}: {customer.Birthday}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.PHONENUMBER)}: {customer.PhoneNumber}");
                Console.WriteLine($"\t{FormatHelper.ToTitleCase(StringConstants.EMAIL)}: {customer.Email}");

                while (true)
                {
                    Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_UPDATE, StringConstants.CUSTOMER));
                    ConsoleHelper.PrintTitleMenu(StringConstants.SELECT_INFORMATION_TO_UPDATE, false);
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
                            customer.Name = newName;
                            break;
                        case "2":
                            DateTime birthday = ConsoleHelper.GetDateTimeInput($"\t{string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.BIRTHDAY)} ({StringConstants.FORMAT_DATE}):", StringConstants.FORMAT_DATE);
                            customer.Birthday = birthday;
                            break;
                        case "3":
                            string phoneNumber = ConsoleHelper.GetStringInput("\t" + string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.PHONENUMBER));
                            customer.PhoneNumber = phoneNumber;
                            break;
                        case "4":
                            string email = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.EMAIL));
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
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.CUSTOMER));
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
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_REMOVE, StringConstants.CUSTOMER));
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
