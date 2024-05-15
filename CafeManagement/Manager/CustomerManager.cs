using System;
using System.Collections.Generic;
using CafeManagement.Models;
using CafeManagement.Services;

public class CustomerManager
{
    private CustomerService _customerService;

    public CustomerManager()
    {
        _customerService = new CustomerService("Data/CustomerData.txt");
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("===== Quản Lý Khách Hàng =====");
            Console.WriteLine("1. Hiển thị danh sách khách hàng");
            Console.WriteLine("2. Thêm khách hàng mới");
            Console.WriteLine("3. Cập nhật thông tin khách hàng");
            Console.WriteLine("4. Xóa khách hàng");
            Console.WriteLine("0. Quay lại");
            Console.Write("Chọn một tùy chọn: ");

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
                    Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                    break;
            }
        }
    }

    private void DisplayAllItems()
    {
        var customers = _customerService.GetAllItems();
        foreach (var customer in customers)
        {
            Console.WriteLine(customer);
        }
    }

    private void Add()
    {
        Console.WriteLine("Nhập thông tin khách hàng mới:");

        Console.Write("Tên khách hàng: ");
        string name = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Số điện thoại: ");
        string phoneNumber = Console.ReadLine();

        _customerService.Add(new Customer(name, email, phoneNumber));
        Console.WriteLine("Khách hàng đã được thêm thành công.");
    }

    private void Update()
    {
        Console.Write("Nhập ID của khách hàng cần cập nhật: ");
        int customerId = int.Parse(Console.ReadLine());

        var customerToUpdate = _customerService.GetById(customerId);
        if (customerToUpdate != null)
        {
            Console.WriteLine($"Khách hàng cần cập nhật: {customerToUpdate}");
            Console.Write("Tên khách hàng mới: ");
            string newName = Console.ReadLine();
            customerToUpdate.Name = newName;

            Console.Write("Email mới: ");
            string newEmail = Console.ReadLine();
            customerToUpdate.Email = newEmail;

            Console.Write("Số điện thoại mới: ");
            string newPhoneNumber = Console.ReadLine();
            customerToUpdate.PhoneNumber = newPhoneNumber;

            _customerService.Update(customerToUpdate);
            Console.WriteLine("Thông tin khách hàng đã được cập nhật.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy khách hàng với ID đã nhập.");
        }
    }

    private void Delete()
    {
        Console.Write("Nhập ID của khách hàng cần xóa: ");
        int customerId = int.Parse(Console.ReadLine());

        _customerService.Delete(customerId);
        Console.WriteLine("Khách hàng đã được xóa.");
    }
}
