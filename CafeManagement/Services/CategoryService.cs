using System;
using CafeManagement.Manager;
using CafeManagement.Models;
using CafeManagement.Utilities;

namespace CafeManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý danh mục sản phẩm.
    /// </summary>
    public class CategoryService
    {
        public LinkedList<Category> Categories;
        private readonly string _filePath;

        /// <summary>
        /// Khởi tạo một đối tượng CategoryService mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp danh sách danh mục.</param>
        public CategoryService(string filePath)
        {
            _filePath = filePath;
            Categories = DataManager.LoadCategories(_filePath);
        }

        /// <summary>
        /// Lấy tất cả các danh mục sản phẩm.
        /// </summary>
        /// <returns>Danh sách các danh mục sản phẩm.</returns>
        public void GetAllItems()
        {
            Categories = DataManager.LoadCategories(_filePath); // Tải danh sách khách hàng từ tệp
        }

        /// <summary>
        /// Tìm một danh mục sản phẩm dựa trên điều kiện được chỉ định.
        /// </summary>
        /// <param name="predicate">Điều kiện để tìm kiếm danh mục sản phẩm.</param>
        /// <returns>Danh mục sản phẩm thỏa mãn điều kiện hoặc null nếu không tìm thấy.</returns>
        public Category Find(Func<Category, bool> predicate)
        {
            Node<Category> category = Categories.Find(predicate);
            if (category != null)
            {
                return category.Data;
            }
            return null;
        }

        /// <summary>
        /// Thêm một danh mục sản phẩm mới.
        /// </summary>
        /// <param name="category">Danh mục sản phẩm cần thêm.</param>
        /// <returns>Danh mục sản phẩm đã được thêm.</returns>
        public Category Add(Category category)
        {
            Category categoryMax = Categories.Max(p => p.Id);
            int maxId = Categories.Count > 0 ? categoryMax.Id : 0;
            category.Id = maxId + 1;
            Categories.AddLast(category);
            DataManager.SaveCategories(_filePath, Categories);
            return category;
        }

        /// <summary>
        /// Cập nhật thông tin của một danh mục sản phẩm.
        /// </summary>
        /// <param name="updatedCategory">Danh mục sản phẩm cần cập nhật.</param>
        public void Update(Category updatedCategory)
        {
            Category category = Find(p => p.Id == updatedCategory.Id);
            if (category != null)
            {
                category.Name = updatedCategory.Name;
                DataManager.SaveCategories(_filePath, Categories);
            }
        }

        /// <summary>
        /// Xóa một danh mục sản phẩm dựa trên mã số.
        /// </summary>
        /// <param name="categoryId">Mã số của danh mục sản phẩm cần xóa.</param>
        public void Delete(int categoryId)
        {
            Node<Category> category = Categories.Find(i => i.Id == categoryId);
            if (category != null)
            {
                Categories.RemoveNode(category);
                DataManager.SaveCategories(_filePath, Categories);
                Console.WriteLine("Sản phẩm đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm với mã số đó.");
            }
        }

        /// <summary>
        /// Lấy thông tin của một danh mục sản phẩm dựa trên mã số.
        /// </summary>
        /// <param name="categoryId">Mã số của danh mục sản phẩm cần lấy.</param>
        /// <returns>Danh mục sản phẩm thỏa mãn mã số hoặc null nếu không tìm thấy.</returns>
        public Category GetById(int categoryId)
        {
            return Categories.Find(p => p.Id == categoryId)?.Data;
        }
    }
}
