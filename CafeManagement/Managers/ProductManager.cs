using System;
using System.Collections.Generic;
using CafeManagement.Constants;
using CafeManagement.Utilities;
using CafeManagement.Models;
using CafeManagement.Services;
using System.Linq;

namespace CafeManagement.Manager
{
    public class ProductManager
    {
        private static ProductService _productService;
        private static CategoryService _categoryService;
        private static OrderService _orderService;
        private static Utilities.LinkedList<Product> _products;
        private static Utilities.LinkedList<Category> _categories;
        private static Utilities.LinkedList<Order> _orders;


        /// <summary>
        /// Khởi tạo một đối tượng mới của lớp ProductManager.
        /// </summary>
        public ProductManager()
        {
            _productService = new ProductService("Data/ProductData.txt");
            _categoryService = new CategoryService("Data/CategoryData.txt");
            _orderService = new OrderService("Data/OrderData.txt");
        }

        /// <summary>
        /// Hiển thị menu quản lý sản phẩm.
        /// </summary>
        public void ShowMenu()
        {
            _productService.GetAllItems();
            _categoryService.GetAllItems();
            _orderService.GetAllItems();
            _products = _productService.Products;
            _categories = _categoryService.Categories;
            _orders = _orderService.Orders;
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.PRODUCT);
                int choice = ConsoleHelper.GetIntInput(StringConstants.ENTER_YOUR_SELECTION);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        DisplayAllItems();
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
                    case 5:
                        DisplayMenu();
                        break;
                    case 0:
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
        /// Hiển thị tất cả sản phẩm.
        /// </summary>
        public void DisplayAllItems()
        {
            ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.PRODUCT));
            ConsoleHelper.PrintHeaderTable(StringConstants.PRODUCT);

            // Sort the products by category and name
            var sortedProducts = _products.ToList().OrderBy(p => p.CategoryId).ThenBy(p => p.Name).ToList();

            int currentCategoryId = -1;
            foreach (var product in sortedProducts)
            {
                if (product.CategoryId != currentCategoryId)
                {
                    if (currentCategoryId != -1)
                    {
                        ConsoleHelper.PrintHorizontalLineOfTable(StringConstants.PRODUCT);
                    }
                    currentCategoryId = product.CategoryId;
                    Category category = _categoryService.GetById(currentCategoryId);
                    Console.WriteLine($"| {category.Name,-51} |"); // Print category name
                }
                Console.WriteLine(product.ToString());
            }
            ConsoleHelper.PrintHorizontalLineOfTable(StringConstants.PRODUCT);
        }

        /// <summary>
        /// Thêm một sản phẩm mới vào hệ thống.
        /// </summary>
        public void Add()
        {
            ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.INPUT_X_NEW, StringConstants.PRODUCT));
            Console.WriteLine(string.Format(StringConstants.LIST_X, StringConstants.CATEGORY));
            foreach (Category category in _categories.ToList())
            {
                Console.WriteLine($"{category.Id}. {category.Name}");
            }
            Console.WriteLine($"0. {string.Format(StringConstants.ADD_X_NEW, StringConstants.CATEGORY)}");
            int categoryId = ConsoleHelper.GetIntInput("Nhập mã loại sản phẩm có trên danh sách (Nhập 0 để tạo mới loại sản phẩm): ");
            Category categoryNew = null;
            if (categoryId == 0)
            {
                string categoryName = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CATEGORY));
                categoryNew = _categoryService.Add(new Category(categoryName));
                categoryId = categoryNew.Id;
            }

            string name = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X, StringConstants.PRODUCT));
            double price = ConsoleHelper.GetDoubleInput("Nhập giá sản phẩm: ");

            _productService.Add(new Product(name, categoryId, price));
            Console.WriteLine(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.PRODUCT);
        }

        /// <summary>
        /// Cập nhật thông tin của một sản phẩm.
        /// </summary>
        public void Update()
        {
            ConsoleHelper.PrintTitleMenu("Cập Nhật Sản Phẩm");
            DisplayAllItems();
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

        /// <summary>
        /// Xóa một sản phẩm khỏi hệ thống.
        /// </summary>
        public void Delete()
        {
            DisplayAllItems();
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

        /// <summary>
        /// Tìm kiếm sản phẩm theo ID.
        /// </summary>
        /// <param name="productId">ID của sản phẩm cần tìm.</param>
        /// <returns>Trả về sản phẩm tương ứng với ID hoặc null nếu không tìm thấy.</returns>
        public Product FindById(int productId)
        {
            Product product = _productService.GetById(productId);
            return product;
        }

        /// <summary>
        /// Kiểm tra xem một sản phẩm có thể được xóa hay không.
        /// </summary>
        /// <param name="Id">ID của sản phẩm cần kiểm tra.</param>
        /// <returns>Trả về true nếu có thể xóa và false nếu sản phẩm có liên kết với đơn hàng.</returns>
        public bool CanDeleteProduct(int Id)
        {
            Node<Order> order = _orders.Find(p => p.Items.Contains(p.Items.Find(items => items.ProductId == Id).Data));
            if (_orders != null)
            {
                return false;
            }
            return true;
        }

        public void DisplayMenu()
        {
            // Sort the products by category
            var sortedProducts = _products.ToList().OrderBy(p => p.CategoryId).ThenBy(p => p.Name).ToList();
            int categoryId = 0;
            foreach (var product in sortedProducts)
            {
                if (product.CategoryId != categoryId)
                {
                    Category category = _categoryService.Find(X => X.Id == product.CategoryId);
                    Console.WriteLine(category.Name.ToUpper());
                    categoryId = category.Id;
                    var productsInCategory = BinarySearchProductsByCategory(sortedProducts, categoryId);
                    foreach (var productInCategory in productsInCategory)
                    {
                        Console.WriteLine($"{productInCategory.Id}. {productInCategory.Name}");
                    }
                    Console.WriteLine();
                }
            }
        }

        private List<Product> BinarySearchProductsByCategory(List<Product> sortedProducts, int category)
        {
            int left = 0;
            int right = sortedProducts.Count - 1;
            List<Product> result = new List<Product>();

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (sortedProducts[mid].CategoryId.Equals(category))
                {
                    // Find the first and last occurrence of the category
                    int first = mid;
                    int last = mid;

                    while (first > 0 && sortedProducts[first - 1].CategoryId.Equals(category))
                    {
                        first--;
                    }

                    while (last < sortedProducts.Count - 1 && sortedProducts[last + 1].CategoryId.Equals(category))
                    {
                        last++;
                    }

                    for (int i = first; i <= last; i++)
                    {
                        result.Add(sortedProducts[i]);
                    }

                    break;
                }
                else if (sortedProducts[mid].CategoryId != category)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return result;
        }
    }
}
