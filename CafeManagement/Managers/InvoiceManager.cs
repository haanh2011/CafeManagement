using System;
using CafeManagement.Constants;
using CafeManagement.Utilities;
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

    /// <summary>
    /// Hiển thị tất cả các hóa đơn.
    /// </summary>
    public void DisplayAllItems()
    {
        LinkedList<Invoice> invoices = _invoiceService.GetAllItems();
        if (invoices.Count > 0)
        {
            ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.INVOICE));
            foreach (Invoice invoice in invoices.ToList())
            {
                Console.WriteLine(invoice.ToString());
            }
        }
        else
        {
            Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.INVOICE));
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
            Console.WriteLine($"Ngày lập hoá đơn: {invoice.Date.ToString(StringConstants.FORMAT_DATETIME)}");
            Console.WriteLine("------------------------------");
            Order order = _orderManager.orderService.GetById(invoice.OrderId);
            _orderManager.DisplayOrder(order);

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Tổng cộng: {FormatHelper.FormatToVND(order.Total())}");
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
                ConsoleHelper.PrintTitleMenu(StringConstants.INVOICE);
                Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
                Console.WriteLine($"Ngày lập hoá đơn: {invoice.Date.ToShortDateString()}");
                Order order = _orderManager.orderService.GetById(invoice.OrderId);
                _orderManager.DisplayOrder(order);

                Console.WriteLine("------------------------------");
                Console.WriteLine($"Tổng cộng: {FormatHelper.FormatToVND(order.Total())}");
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
    public void Add()
    {
        Console.WriteLine(string.Format(StringConstants.ENTER_THE_INFORMATION_OF_X_TO_ADD, StringConstants.INVOICE)); // Yêu cầu nhập thông tin khách hàng
        Console.WriteLine(); // Yêu cầu nhập thông tin khách hàng
        int orderId = ConsoleHelper.GetIntInput(string.Format(StringConstants.INPUT_X, StringConstants.ORDER));
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
        Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.INVOICE));
        PrintInvoice(invoice);
    }

    /// <summary>
    /// Cập nhật hóa đơn.
    /// </summary>
    public void Update()
    {
        int invoiceId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_UPDATE, StringConstants.INVOICE));
        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice == null)
        {
            Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.INVOICE));
            return;
        }
        int orderId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_UPDATE, StringConstants.INVOICE));
        invoice.OrderId = orderId;
        invoice.Date = DateTime.Now;
        _invoiceService.Update(invoice);
        Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_UPDATE, StringConstants.INVOICE));

    }

    /// <summary>
    /// Xóa hóa đơn.
    /// </summary>
    public void Delete()
    {
        int invoiceId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.INVOICE));
        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice == null)
        {
            Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.INVOICE));
            return;
        }
        _invoiceService.Delete(invoiceId);
        Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_DELETE, StringConstants.INVOICE));
    }
}
