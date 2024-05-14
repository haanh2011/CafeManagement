using System.Collections.Generic;
using CafeManagement.Data;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class CategoryService
    {
        private readonly string _filePath;
        private readonly List<Category> _categories;

        public CategoryService(string filePath, List<Category> categories)
        {
            _filePath = filePath;
            _categories = categories;
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
