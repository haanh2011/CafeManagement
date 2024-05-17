using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CategoryService
    {
        private List<Category> _categories;
        private readonly string _filePath;

        public CategoryService(string filePath)
        {
            _filePath = filePath;
            _categories = DataManager.LoadCategories(filePath);
        }

        public List<Category> GetAllItems()
        {
            return _categories;
        }

        public int Add(string nameCategory)
        {
            Category category = new Category(nameCategory);
            // Tìm mã số lớn nhất hiện tại
            int maxId = _categories.Count > 0 ? _categories.Max(c => c.Id) : 0;
            // Gán mã số mới cho category
            category.Id = maxId + 1;
            _categories.Add(category);
            DataManager.SaveCategories(_filePath, _categories);
            return category.Id;
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
            DataManager.SaveCategories(_filePath, _categories);
        }

        public void UpdateCategory(Category updatedCategory)
        {
            for (int i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].Id == updatedCategory.Id)
                {
                    _categories[i] = updatedCategory;
                    break;
                }
            }
            DataManager.SaveCategories(_filePath, _categories);
        }

        public void DeleteCategory(int categoryId)
        {
            _categories.RemoveAll(c => c.Id == categoryId);
            DataManager.SaveCategories(_filePath, _categories);
        }
    }
}
