using CafeManagement.Constants;
using CafeManagement.Helpers;
using CafeManagement.Models;
using CafeManagement.Services;
using System;

namespace CafeManagement.Manager
{
    public class CategoryManager
    {
        private CategoryService _categoryService; // Dịch vụ quản lý danh mục sản phẩm
        private ProductService _productService; // Dịch vụ quản lý sản phẩm

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp CategoryManager.
        /// </summary>
        public CategoryManager()
        {
            _categoryService = new CategoryService("Data/CategoryData.txt"); // Khởi tạo dịch vụ quản lý danh mục sản phẩm
            _productService = new ProductService("Data/ProductData.txt"); // Khởi tạo dịch vụ quản lý sản phẩm
        }

        /// <summary>
        /// Hiển thị menu quản lý danh mục sản phẩm và xử lý lựa chọn từ người dùng.
        /// </summary>
        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.CATEGORY); // In ra menu quản lý danh mục sản phẩm
                var choice = Console.ReadLine(); // Nhận lựa chọn từ người dùng
                Console.WriteLine();
                switch (choice)
                {
                    case "1":
                        DisplayAllItems(); // Hiển thị tất cả các danh mục
                        break;
                    case "2":
                        Add(); // Thêm danh mục mới
                        break;
                    case "3":
                        Update(); // Cập nhật danh mục
                        break;
                    case "4":
                        Delete(); // Xóa danh mục
                        break;
                    case "0":
                        return; // Thoát khỏi menu
                    default:
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION); // Thông báo lựa chọn không hợp lệ
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU); // Hướng dẫn trở lại menu
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Hiển thị tất cả các mục danh mục sản phẩm.
        /// </summary>
        private void DisplayAllItems()
        {
            LinkedList<Category> categories = _categoryService.GetAllItems(); // Lấy danh sách tất cả các danh mục sản phẩm
            if (categories.Count > 0)
            {
                ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.CATEGORY)); // In tiêu đề danh sách danh mục sản phẩm
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST, StringConstants.CATEGORY)); // Hiển thị thông báo không có danh mục sản phẩm
            }
            foreach (Category category in categories.ToList())
            {
                Console.WriteLine(category.ToString()); // In thông tin của từng danh mục sản phẩm
            }
        }

        /// <summary>
        /// Thêm một danh mục sản phẩm mới.
        /// </summary>
        public void Add()
        {
            // Yêu cầu nhập tên danh mục mới
            Console.WriteLine(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CATEGORY)); 
            // Nhận tên danh mục từ người dùng
            string name = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: "); 
            // Tìm kiếm danh mục theo tên
            Category category = _categoryService.Find(item => item.Name.ToUpper() == name.ToUpper()); 
            if (category != null)
            {
                // Thông báo nếu danh mục đã tồn tại
                Console.WriteLine(string.Format(StringConstants.X_IS_EXIST_IN_LIST, StringConstants.CATEGORY)); 
                return;
            }
            // Thêm danh mục mới vào danh sách
            _categoryService.Add(new Category(name)); 
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.CATEGORY)); // Hiển thị thông báo thành công
        }
        /// <summary>
        /// Cập nhật thông tin của một danh mục sản phẩm.
        /// </summary>
        public void Update()
        {
            DisplayAllItems(); // Hiển thị danh sách danh mục
            int categoryId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_UPDATE, StringConstants.CATEGORY)); // Nhập mã số của danh mục cần cập nhật

            Category category = _categoryService.GetById(categoryId); // Tìm danh mục theo mã số
            if (category != null)
            {
                string newName = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CATEGORY)); // Nhập tên mới cho danh mục
                category.Name = newName; // Cập nhật tên của danh mục
                _categoryService.Update(category); // Lưu thay đổi
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.CATEGORY)); // Thông báo nếu không tìm thấy danh mục
            }
        }

        /// <summary>
        /// Xóa một danh mục sản phẩm.
        /// </summary>
        public void Delete()
        {
            DisplayAllItems(); // Hiển thị danh sách danh mục
            int categoryId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.CATEGORY)); // Nhập mã số của danh mục cần xóa
            if (!CanDeleteCategory(categoryId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.CATEGORY, StringConstants.PRODUCT)); // Kiểm tra xem có thể xóa danh mục không
                return;
            }
            _categoryService.Delete(categoryId); // Xóa danh mục
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_REMOVE, StringConstants.CATEGORY)); // Thông báo đã xóa thành công
        }

        /// <summary>
        /// Tìm một danh mục sản phẩm theo mã số.
        /// </summary>
        /// <param name="categoryId">Mã số của danh mục cần tìm.</param>
        /// <returns>Danh mục sản phẩm tìm thấy, nếu không tìm thấy trả về null.</returns>
        public Category FindById(int categoryId)
        {
            Category category = _categoryService.GetById(categoryId); // Tìm danh mục theo mã số
            return category;
        }

        /// <summary>
        /// Kiểm tra xem một danh mục có thể được xóa không.
        /// </summary>
        /// <param name="categoryId">Mã số của danh mục cần kiểm tra.</param>
        /// <returns>Trả về true nếu danh mục có thể được xóa, ngược lại trả về false.</returns>
        public bool CanDeleteCategory(int categoryId)
        {
            LinkedList<Product> products = _productService.GetAllItems(); // Lấy danh sách sản phẩm
            Node<Product> product = products.Find(p => p.CategoryId == categoryId); // Tìm sản phẩm thuộc danh mục cần kiểm tra
            if (product != null)
            {
                return false; // Nếu có sản phẩm thuộc danh mục này, không thể xóa
            }
            return true; // Nếu không có sản phẩm thuộc danh mục này, có thể xóa
        }
    }
}
