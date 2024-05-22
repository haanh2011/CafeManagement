using System;
using System.Text;
using CafeManagement.Constants;
using CafeManagement.Utilities;
using CafeManagement.Manager;

namespace CafeManagement
{
    class Program
    {
        // Tạo các đối tượng quản lý
        static CategoryManager categoryManager = new CategoryManager();
        static ProductManager productManager = new ProductManager();
        static OrderManager orderManager = new OrderManager();
        static InvoiceManager invoiceManager = new InvoiceManager();
        static CustomerManager customerManager = new CustomerManager();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            while (true)
            {
                ConsoleHelper.PrintTitleMenu(StringConstants.TITLE_PROJECT);
                Console.WriteLine("1. " + FormatHelper.ToTitleCase(string.Format(StringConstants.MANAGE_X, StringConstants.CATEGORY)));
                Console.WriteLine("2. " + FormatHelper.ToTitleCase(string.Format(StringConstants.MANAGE_X, StringConstants.PRODUCT)));
                Console.WriteLine("3. " + FormatHelper.ToTitleCase(string.Format(StringConstants.MANAGE_X, StringConstants.ORDER)));
                Console.WriteLine("4. " + FormatHelper.ToTitleCase(string.Format(StringConstants.MANAGE_X, StringConstants.INVOICE)));
                Console.WriteLine("5. " + FormatHelper.ToTitleCase(string.Format(StringConstants.MANAGE_X, StringConstants.CUSTOMER)));
                Console.WriteLine("0. Thoát");

                int choice = ConsoleHelper.GetIntInput(StringConstants.ENTER_YOUR_SELECTION);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        categoryManager.ShowMenu();
                        break;
                    case 2:
                        productManager.ShowMenu();
                        break;
                    case 3:
                        orderManager.ShowMenu();
                        break;
                    case 4:
                        invoiceManager.ShowMenu();
                        break;
                    case 5:
                        customerManager.ShowMenu();
                        break;
                    case 0:
                        Console.WriteLine("Tạm biệt!");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                        break;
                }
            }
        }
    }
}
