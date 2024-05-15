using CafeManagement.Helpers;
using CafeManagement.Models;
using CafeManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Manager
{
    public class ProductManager
    {
        private static ProductService _productService;
        private static CategoryService _categoryService;

        public ProductManager()
        {
            _productService = new ProductService("Data/ProductData.txt");
            _categoryService = new CategoryService("Data/ProductData.txt");
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("===== Quản Lý Sản Phẩm =====");
                Console.WriteLine("1. Hiển thị danh sách sản phẩm");
                Console.WriteLine("2. Thêm sản phẩm");
                Console.WriteLine("3. Cập nhật sản phẩm");
                Console.WriteLine("4. Xóa sản phẩm");
                Console.WriteLine("5. Tìm kiếm sản phẩm theo mã");
                Console.WriteLine("0. Quay lại");

                int choice = ConsoleHelper.GetIntInput("Nhập lựa chọn của bạn: ");

                switch (choice)
                {
                    case 1:
                        ShowAllItems();
                        break;
                    case 2:
                        Add();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            }
        }

        public void ShowAllItems()
        {
            Console.WriteLine("===== Danh Sách Sản Phẩm =====");
            List <Product> products = _productService.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}. {product.Name} - {product.Price}");
            }
            Console.WriteLine();
        }

        public void Add()
        {
            Console.WriteLine("===== Thêm Sản Phẩm Mới =====");
            string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm: ");
            double price = ConsoleHelper.GetDoubleInput("Nhập giá sản phẩm: ");

            Console.WriteLine("Danh sách các loại sản phẩm có sẵn:");
            List<Category> categories = _categoryService.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }
            Console.WriteLine("0. Thêm loại sản phẩm mới");
            int categoryId = ConsoleHelper.GetIntInput("Chọn loại sản phẩm: ");
            if (categoryId == 0)
            {
                string categoryName = ConsoleHelper.GetStringInput("Nhập tên loại sản phẩm: ");
                categoryId = _categoryService.AddCategory(categoryName);
            }
            _productService.Add(new Product(name, categoryId, price));
            Console.WriteLine("Đã thêm sản phẩm thành công!");
        }

        public void Update()
        {
            Console.WriteLine("===== Cập Nhật Sản Phẩm =====");
            ShowAllItems();
            int productId = ConsoleHelper.GetIntInput("Nhập ID sản phẩm cần cập nhật: ");

            var product = _productService.GetById(productId);
            if (product != null)
            {
                string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm mới: ");
                double price = ConsoleHelper.GetDoubleInput("Nhập giá sản phẩm mới: ");

                product.Name = name;
                product.Price = price;

                _productService.Update(product);
                Console.WriteLine("Đã cập nhật sản phẩm thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm!");
            }
        }

        public void Delete()
        {
            Console.WriteLine("===== Xóa Sản Phẩm =====");
            ShowAllItems();
            int productId = ConsoleHelper.GetIntInput("Nhập ID sản phẩm cần xóa: ");

            Product product = _productService.GetById(productId);
            if (product != null)
            {
                _productService.Delete(productId);
                Console.WriteLine("Đã xóa sản phẩm thành công!");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm!");
            }
        }
    }
}
