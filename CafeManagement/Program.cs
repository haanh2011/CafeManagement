using System;
using System.Text;
using CafeManagement.Constants;
using CafeManagement.Utilities;
using CafeManagement.Manager;
using System.Runtime.InteropServices;

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
        const int STD_OUTPUT_HANDLE = -11;
        const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        static void Main(string[] args)
        {
            // Bật hỗ trợ ANSI
            if (EnableVirtualTerminalProcessing())
            {
                Console.WriteLine("\x1b[32mConsole hỗ trợ các lệnh điều khiển ANSI.\x1b[0m");
            }
            else
            {
                Console.WriteLine("Console không hỗ trợ các lệnh điều khiển ANSI.");
            }
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Console.Title = StringConstants.TITLE_PROJECT;
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
        static bool EnableVirtualTerminalProcessing()
        {
            IntPtr handle = GetStdHandle(STD_OUTPUT_HANDLE);
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            if (!GetConsoleMode(handle, out uint mode))
            {
                return false;
            }

            mode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(handle, mode))
            {
                return false;
            }

            return true;
        }
    }
}
