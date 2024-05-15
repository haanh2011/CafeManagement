using System;
using System.Collections.Generic;
using CafeManagement.Models;
using CafeManagement.Services;

public class InvoiceManager
{
    private ProductService _productService;
    private CustomerService _customerService;
    private InvoiceService _invoiceService;
    private OrderService _orderService;

    public InvoiceManager()
    {
        _productService = new ProductService("Data/ProductData.txt");
        _invoiceService = new InvoiceService("Data/InvoiceData.txt");
        _customerService = new CustomerService("Data/CustomerData.txt");
        _orderService = new OrderService("Data/OrderData.txt");
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Quản Lý Hoá Đơn =====");
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
            Console.WriteLine("===== Hóa Đơn =====");
            Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
            Console.WriteLine($"Mã đơn hàng: {invoice.OrderId}");
            Console.WriteLine($"Ngày lập: {invoice.Date.ToShortDateString()}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Sản phẩm            Đơn giá   Số lượng   Thành tiền");
            Console.WriteLine("------------------------------");

            var order = _orderService.GetById(invoice.OrderId);
            foreach (var item in order.Items)
            {
                Product product = _productService.GetById(item.ProductId);
                Console.WriteLine($"{product.Name,-20} {item.UnitPrice,9:C} {item.Quantity,9} {item.UnitPrice * item.Quantity,12:C}");
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine($"Tổng cộng: {invoice.Total:C}");
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

            if (invoice != null)
            {
                Console.WriteLine("===== Hóa Đơn =====");
                Console.WriteLine($"Mã hóa đơn: {invoice.Id}");
                Console.WriteLine($"Mã đơn hàng: {invoice.OrderId}");
                Console.WriteLine($"Ngày lập: {invoice.Date.ToShortDateString()}");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Sản phẩm            Đơn giá   Số lượng   Thành tiền");
                Console.WriteLine("------------------------------");

                var order = _orderService.GetById(invoice.OrderId);
                foreach (var item in order.Items)
                {
                    Product product = _productService.GetById(item.ProductId);
                    Console.WriteLine($"{product.Name,-20} {item.UnitPrice,9:C} {item.Quantity,9} {item.UnitPrice * item.Quantity,12:C}");
                }

                Console.WriteLine("------------------------------");
                Console.WriteLine($"Tổng cộng: {invoice.Total:C}");
                Console.WriteLine("------------------------------");
                Console.WriteLine("Cảm ơn quý khách và hẹn gặp lại!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy hóa đơn.");
            }
        }

    }
    private void Create()
    {
        Console.WriteLine("Nhập thông tin hóa đơn mới:");

        Console.Write("Nhập mã đơn hàng: ");
        int orderId = int.Parse(Console.ReadLine());

        Console.Write("Nhập tổng giá trị: ");
        double total = double.Parse(Console.ReadLine());

        // Tính số điểm từ tổng tiền (1000 = 1 point)
        int pointsEarned = (int)(total / 1000);

        // Thêm số điểm tính được vào điểm tích lũy của khách hàng
        _customerService.AddPoints(pointsEarned);

        // Tạo hóa đơn mới
        DateTime currentDate = DateTime.Now;
        Invoice invoice = new Invoice(0, orderId, total, currentDate);

        int invoiceId = _invoiceService.Create(invoice);
        invoice.Id = invoiceId;
        Console.WriteLine("Hóa đơn đã được tạo thành công.");
        PrintInvoice(invoice);
    }

    private void Update()
    {
        Console.Write("Nhập ID của hoá đơn cần cập nhật: ");
        int invoiceId = int.Parse(Console.ReadLine());

        var invoiceToUpdate = _invoiceService.GetById(invoiceId);
        if (invoiceToUpdate != null)
        {
            Console.WriteLine($"Hoá đơn cần cập nhật: {invoiceToUpdate}");
            Console.Write("Nhập tổng giá trị mới: ");
            double newTotal = double.Parse(Console.ReadLine());
            invoiceToUpdate.Total = newTotal;

            _invoiceService.Update(invoiceToUpdate);
            Console.WriteLine("Hoá đơn đã được cập nhật.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy hoá đơn với ID đã nhập.");
        }
    }

    private void Delete()
    {
        Console.Write("Nhập ID của hoá đơn cần xóa: ");
        int invoiceId = int.Parse(Console.ReadLine());

        var invoiceToDelete = _invoiceService.GetById(invoiceId);
        if (invoiceToDelete != null)
        {
            _invoiceService.Delete(invoiceId);
            Console.WriteLine("Hoá đơn đã được xóa.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy hoá đơn với ID đã nhập.");
        }
    }
}
