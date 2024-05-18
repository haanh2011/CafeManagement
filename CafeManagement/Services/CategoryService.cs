using System;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CategoryService
    {
        private LinkedList<Category> _categories;
        private readonly string _filePath;

        public CategoryService(string filePath)
        {
            _filePath = filePath;
            _categories = DataManager.LoadCategories(_filePath);
        }
        public LinkedList<Category> GetAllItems()
        {
            return _categories;
        }

        public Category Find(Func<Category, bool> predicate)
        {
            Node<Category> category = _categories.Find(predicate);
            if (category != null)
            {
                return category.Data;
            }
            return null;
        }

        public Category Add(Category category)
        {
            Category categoryMax = _categories.Max(p => p.Id);
            int maxId = _categories.Count > 0 ? categoryMax.Id : 0;
            category.Id = maxId + 1;
            _categories.AddLast(category);
            DataManager.LoadCategories(_filePath);
            return category;
        }

        public void Update(Category updatedCategory)
        {
            Category category = Find(p => p.Id == updatedCategory.Id);
            if (category != null)
            {
                category.Name = updatedCategory.Name;
                DataManager.LoadCategories(_filePath);
            }
        }

        public void Delete(int categoryId)
        {
            Node<Category> category = _categories.Find(i => i.Id == categoryId);
            if (category != null)
            {
                _categories.RemoveNode(category);
                DataManager.LoadCategories(_filePath);
                Console.WriteLine("Sản phẩm đã được xóa.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm với mã số đó.");
            }
        }

        public Category GetById(int categoryId)
        {
            return _categories.Find(p => p.Id == categoryId)?.Data;
        }
    }
}
