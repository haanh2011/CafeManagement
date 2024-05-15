using System;
using System.Text;
using CafeManagement.Helpers;
using CafeManagement.Manager;

namespace CafeManagement
{
    class Program
    {
        // Tạo các đối tượng quản lý
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
                        productManager.ShowMenu();
                        break;
                    case 2:
                        orderManager.ShowMenu();
                        break;
                    case 3:
                        invoiceManager.ShowMenu();
                        break;
                    case 4:
                        customerManager.ShowMenu();
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
    }
}
