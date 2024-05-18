using System;
using CafeManagement.Constants;
using CafeManagement.Helpers;
using CafeManagement.Models;
using CafeManagement.Services;

namespace CafeManagement.Manager
{
    public class ProductManager
    {
        private static ProductService _productService;
        private static CategoryService _categoryService;
        private static OrderService _orderService;

        public ProductManager()
        {
            _productService = new ProductService("Data/ProductData.txt");
            _categoryService = new CategoryService("Data/CategoryData.txt");
            _orderService = new OrderService("Data/OrderData.txt");
        }

        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.PRODUCT);
                int choice = ConsoleHelper.GetIntInput(StringConstants.ENTER_YOUR_SELECTION);
                Console.WriteLine();

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
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU);
                Console.ReadLine();
            }
        }

        public void ShowAllItems()
        {
            ConsoleHelper.PrintTitleMenu("Danh Sách Sản Phẩm");
            Models.LinkedList<Product> products = _productService.GetAllItems();
            foreach (Product product in products.ToList())
            {
                Console.WriteLine($"{product.Id}. {product.Name} - {product.Price} {StringConstants.CURRENCT_UNIT}");
            }
            Console.WriteLine();
        }

        public void Add()
        {
            ConsoleHelper.PrintTitleMenu("Thêm Sản Phẩm Mới");
            Console.WriteLine("Danh sách các loại sản phẩm có sẵn:");
            Models.LinkedList<Category> categories = _categoryService.GetAllItems();
            foreach (Category category in categories.ToList())
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }
            Console.WriteLine("0. Thêm loại sản phẩm mới");
            int categoryId = ConsoleHelper.GetIntInput("Chọn loại sản phẩm: ");
            Category categoryNew = null;
            if (categoryId == 0)
            {
                string categoryName = ConsoleHelper.GetStringInput("Nhập tên loại sản phẩm: ");
                categoryNew = _categoryService.Add(new Category(categoryName));
            }

            string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm: ");
            double price = ConsoleHelper.GetDoubleInput("Nhập giá sản phẩm: ");

            _productService.Add(new Product(name, categoryNew.Id, price));
            Console.WriteLine("Đã thêm sản phẩm thành công!");
        }

        public void Update()
        {
            ConsoleHelper.PrintTitleMenu("Cập Nhật Sản Phẩm");
            ShowAllItems();
            int productId = ConsoleHelper.GetIntInput("Nhập ID sản phẩm cần cập nhật: ");
            Product product = _productService.GetById(productId);
            if (product != null)
            {
                string name = ConsoleHelper.GetStringInput("Nhập tên sản phẩm mới: ");
                double price = ConsoleHelper.GetDoubleInput("Nhập giá sản phẩm mới: ");

                product.Name = name;
                product.Price = price;

                _productService.Update(product);
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm!");
            }
        }

        public void Delete()
        {
            ShowAllItems();
            int productId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.PRODUCT));
            if (!CanDeleteProduct(productId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.PRODUCT, StringConstants.ORDER));
                return;
            }
            Product product = FindById(productId);
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

        public Product FindById(int productId)
        {
            Product product = _productService.GetById(productId);
            return product;
        }

        public bool CanDeleteProduct(int Id)
        {
            LinkedList<Order> orders = _orderService.GetAllItems();
            Node<Order> order = orders.Find(p => p.Items.Contains(p.Items.Find(items => items.ProductId == Id).Data));
            if (orders != null)
            {
                return false;
            }
            return true;
        }
    }
}
