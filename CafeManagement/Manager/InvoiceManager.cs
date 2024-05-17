using System;
using System.Collections.Generic;
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

    public void ShowMenu()
    {
        while (true)
        {
            ConsoleHelper.PrintTitleMenu("Quản Lý Hoá Đơn");
            Console.WriteLine("1. Hiển thị danh sách hoá đơn");
            Console.WriteLine("2. Tạo hoá đơn mới");
            Console.WriteLine("3. Cập nhật hoá đơn");
            Console.WriteLine("4. Xóa hoá đơn");
            Console.WriteLine("0. Quay lại");
            Console.Write("Chọn một tùy chọn: ");

            var choice = Console.ReadLine();
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
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }

    private void DisplayAllItems()
    {
        var invoices = _invoiceService.GetAllItems();
        foreach (var invoice in invoices)
        {
            Console.WriteLine(invoice);
        }
    }

    public void PrintInvoice(Invoice invoice)
    {
        // Hỏi người dùng có muốn xuất hóa đơn không
        Console.Write("Bạn có muốn in hóa đơn không? (Y/N): ");
        string answer = Console.ReadLine();
        if (answer.ToUpper() == "Y")
        {
            ConsoleHelper.PrintTitleMenu("Hóa Đơn");
            Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
            Console.WriteLine($"Mã đơn hàng: {invoice.OrderId}");
            Console.WriteLine($"Ngày lập: {invoice.Date.ToShortDateString()}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Sản phẩm            Đơn giá   Số lượng   Thành tiền");
            Console.WriteLine("------------------------------");

            var order = _orderManager.orderService.GetById(invoice.OrderId);
            foreach (var item in order.Items)
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

    public void PrintInvoiceById(int invoiceId)
    {
        // Hỏi người dùng có muốn xuất hóa đơn không
        Console.Write("Bạn có muốn in hóa đơn không? (Y/N): ");
        string answer = Console.ReadLine();

        if (answer.ToUpper() == "Y")
        {
            Invoice invoice = _invoiceService.GetById(invoiceId);

            if (invoice.Equals(default(Invoice)))
            {
                Console.WriteLine("Không tìm thấy hóa đơn.");
            }
            else
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
        }

    }
    private void Create()
    {
        Console.WriteLine("Nhập thông tin hóa đơn mới:");
        int orderId = ConsoleHelper.GetIntInput("Nhập mã đơn hàng: ");
        double total = ConsoleHelper.GetDoubleInput("Nhập tổng giá trị: ");

        // Tính số điểm từ tổng tiền (1000 = 1 point)
        int pointsEarned = (int)(total / 1000);

        // Thêm số điểm tính được vào điểm tích lũy của khách hàng
        _orderManager.customerService.AddPoints(pointsEarned);

        // Tạo hóa đơn mới
        DateTime currentDate = DateTime.Now;
        Invoice invoice = new Invoice(0, orderId, currentDate);

        int invoiceId = _invoiceService.Create(invoice);
        invoice.Id = invoiceId;
        Console.WriteLine("Hóa đơn đã được tạo thành công.");
        PrintInvoice(invoice);
    }

    private void Update()
    {
        Console.Write("Nhập ID của hoá đơn cần cập nhật: ");
        int invoiceId = ConsoleHelper.GetIntInput("Nhập ID của hoá đơn cần cập nhật: ");
        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice.Equals(default(Invoice)))
        {
            Console.WriteLine("Không tìm thấy hoá đơn với ID đã nhập.");
        }
        else
        {
            int orderId = ConsoleHelper.GetIntInput("Nhập vào mã đơn hàng bạn muốn thay đổi: ");
            invoice.OrderId = orderId;
            invoice.Date = DateTime.Now;
            _invoiceService.Update(invoice);
            Console.WriteLine("Hoá đơn đã được cập nhật.");
        }
    }

    private void Delete()
    {
        Console.Write("Nhập ID của hoá đơn cần xóa: ");
        int invoiceId = int.Parse(Console.ReadLine());

        Invoice invoice = _invoiceService.GetById(invoiceId);
        if (invoice.Equals(default(Invoice)))
        {
            Console.WriteLine("Không tìm thấy hoá đơn với ID đã nhập.");

        }
        else
        {
            _invoiceService.Delete(invoiceId);
            Console.WriteLine("Hoá đơn đã được xóa.");
        }
    }
}
