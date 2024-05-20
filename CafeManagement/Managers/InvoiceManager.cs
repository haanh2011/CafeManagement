using System;
using CafeManagement.Constants;
using CafeManagement.Helpers;
using CafeManagement.Manager;
using CafeManagement.Models;
using CafeManagement.Services;

public class InvoiceManager
{
    private OrderManager _orderManager;
    private InvoiceService _invoiceService;

    public InvoiceManager()
    {
        _invoiceService = new InvoiceService("Data/InvoiceData.txt");
        _orderManager = new OrderManager();
    }

    /// <summary>
    /// Hiển thị menu quản lý hóa đơn.
    /// </summary>
    public void ShowMenu()
    {
        while (true)
        {
            ConsoleHelper.PrintMenuDetails(StringConstants.INVOICE);
            var choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    DisplayAllItems();
                    break;
                case "2":
                    Create();
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

    /// <summary>
    /// Hiển thị tất cả các hóa đơn.
    /// </summary>
    public void DisplayAllItems()
    {
        LinkedList<Invoice> invoices = _invoiceService.GetAllItems();
        if (invoices.Count > 0)
        {
            ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.INVOICE));
        }
        else
        {
            Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.INVOICE));
        }
        foreach (Invoice invoice in invoices.ToList())
        {
            Console.WriteLine(invoice.ToString());
        }
    }

    /// <summary>
    /// In hóa đơn.
    /// </summary>
    /// <param name="invoice">Hóa đơn cần in.</param>
    public void PrintInvoice(Invoice invoice)
    {
        // Hỏi người dùng có muốn xuất hóa đơn không
        Console.Write("Bạn có muốn in hóa đơn không? (Y/N): ");
        string answer = Console.ReadLine();
        if (answer.ToUpper() == "Y")
        {
            // In hóa đơn
            ConsoleHelper.PrintTitleMenu("Hóa Đơn");
            Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
            Console.WriteLine($"Mã đơn hàng: {invoice.OrderId}");
            Console.WriteLine($"Ngày lập: {invoice.Date.ToShortDateString()}");
            // Lấy thông tin đơn hàng từ OrderManager
            Order order = _orderManager.orderService.GetById(invoice.OrderId);
            foreach (OrderItem item in order.Items.ToList())
            {
                Product product = _orderManager.productService.GetById(item.ProductId);
                Console.WriteLine($"{product.Name,-20} {item.UnitPrice,9:C} {item.Quantity,9} {item.UnitPrice * item.Quantity,12:C}");
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Tổng cộng: {order.Total():C}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Cảm ơn quý khách và hẹn gặp lại!");
        }

    }

    /// <summary>
    /// In hóa đơn theo ID.
    /// </summary>
    /// <param name="invoiceId">ID của hóa đơn cần in.</param>
    public void PrintInvoiceById(int invoiceId)
    {
        // Hỏi người dùng có muốn xuất hóa đơn không
        Console.Write("Bạn có muốn in hóa đơn không? (Y/N): ");
        string answer = Console.ReadLine();

        if (answer.ToUpper() == "Y")
        {
            Invoice invoice = _invoiceService.GetById(invoiceId);

            if (invoice != null)
            {
                ConsoleHelper.PrintTitleMenu("Hóa Đơn");
                Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
                Console.WriteLine($"Mã đơn hàng: {invoice.OrderId}");
                Console.WriteLine($"Ngày lập: {invoice.Date.ToShortDateString()}");
                Order order = _orderManager.orderService.GetById(invoice.OrderId);
                _orderManager.DisplayOrder(order);

                Console.WriteLine("------------------------------");
                Console.WriteLine($"Tổng cộng: {order.Total():C}");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Cảm ơn quý khách và hẹn gặp lại!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy hóa đơn.");
            }
        }

    }
    /// <summary>
    /// Tạo hóa đơn mới.
    /// </summary>
    public void Create()
    {
        Console.WriteLine("Nhập thông tin hóa đơn mới:");
        int orderId = ConsoleHelper.GetIntInput("Nhập mã đơn hàng: ");
        Order order = _orderManager.orderService.GetById(orderId);

        if (order == null)
        {
            Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.ORDER));
            return;
        }
        // Tính số điểm từ tổng tiền (1000 = 1 point)
        int pointsEarned = (int)(order.Total() / 1000);

        // Thêm số điểm tính được vào điểm tích lũy của khách hàng
        _orderManager.customerService.AddPoints(pointsEarned);

        // Tạo hóa đơn
        DateTime currentDate = DateTime.Now;
        Invoice invoice = new Invoice(0, orderId, currentDate);

        invoice = _invoiceService.Add(invoice);
        Console.WriteLine("Hóa đơn đã được tạo thành công.");
        PrintInvoice(invoice);
    }

    /// <summary>
    /// Cập nhật hóa đơn.
    /// </summary>
    public void Update()
    {
        Console.Write("Nhập ID của hóa đơn cần cập nhật: ");
        int invoiceId = ConsoleHelper.GetIntInput("Nhập ID của hóa đơn cần cập nhật: ");
        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice == null)
        {
            Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.INVOICE));
            return;
        }
        int orderId = ConsoleHelper.GetIntInput("Nhập vào mã đơn hàng bạn muốn thay đổi: ");
        invoice.OrderId = orderId;
        invoice.Date = DateTime.Now;
        _invoiceService.Update(invoice);
        Console.WriteLine("Hóa đơn đã được cập nhật.");
    }

    /// <summary>
    /// Xóa hóa đơn.
    /// </summary>
    public void Delete()
    {
        Console.Write("Nhập ID của hóa đơn cần xóa: ");
        int invoiceId = int.Parse(Console.ReadLine());

        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice == null)
        {
            Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.INVOICE));
            return;
        }
        _invoiceService.Delete(invoiceId);
        Console.WriteLine("Hóa đơn đã được xóa.");
    }
}

}
