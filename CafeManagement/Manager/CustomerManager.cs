using System;
using System.Collections.Generic;
using CafeManagement.Helpers;
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
            ConsoleHelper.PrintTitleMenu("Quản lý khách hàng");
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
        Console.Write("Nhập thông tin khách hàng mới:");
        string name = ConsoleHelper.GetStringInput("\tTên: ");
        DateTime birthday = ConsoleHelper.GetDateTimeInput("\tNgày sinh: ");
        string phoneNumber = ConsoleHelper.GetStringInput("\tSố điện thoại: ");
        string email = ConsoleHelper.GetStringInput("\tEmail: ");

        _customerService.Add(new Customer(name, birthday, phoneNumber, email));
        Console.WriteLine("Khách hàng đã được thêm thành công.");
    }

    private void Update()
    {
        int customerId = ConsoleHelper.GetIntInput("Nhập ID của khách hàng cần cập nhật: ");

        Customer customer = _customerService.GetById(customerId);
        if (customer.Equals(default(Customer)))
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
                Console.WriteLine("0. Quay lại");
                Console.Write("Chọn một tùy chọn: ");

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
                        Console.WriteLine("Tùy chọn không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy khách hàng với ID đã nhập.");
        }
    }

    private void Delete()
    {
        int customerId = ConsoleHelper.GetIntInput("Nhập ID của khách hàng cần xóa: ");
        _customerService.Delete(customerId);
        Console.WriteLine("Khách hàng đã được xóa.");
    }
}
