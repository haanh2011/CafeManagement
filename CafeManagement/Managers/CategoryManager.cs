using CafeManagement.Constants;
using CafeManagement.Helpers;
using CafeManagement.Models;
using CafeManagement.Services;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CafeManagement.Manager
{
    public class CategoryManager
    {
        private CategoryService _categoryService;
        private ProductService _productService;

        public CategoryManager()
        {
            _categoryService = new CategoryService("Data/CategoryData.txt");
            _productService = new ProductService("Data/ProductData.txt");
        }

        public void ShowMenu()
        {
            while (true)
            {
                ConsoleHelper.PrintMenuDetails(StringConstants.CATEGORY);
                var choice = Console.ReadLine();
                Console.WriteLine();
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
                        Console.WriteLine(StringConstants.MESSAGE_INVALID_OPTION);
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(StringConstants.ENTER_THE_KEY_ENTER_TO_RETURN_TO_THE_MENU);
                Console.ReadLine();
            }
        }

        private void DisplayAllItems()
        {
            LinkedList<Category> catogorys = _categoryService.GetAllItems();
            if (catogorys.Count > 0)
            {
                ConsoleHelper.PrintTitleMenu(string.Format(StringConstants.LIST_X, StringConstants.CATEGORY));
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.THERE_ARE_NO_X_IN_THE_LIST,StringConstants.CATEGORY));
            }
            foreach (Category catogory in catogorys.ToList())
            {
                Console.WriteLine(catogory.ToString());
            }
        }

        public void Add()
        {
            Console.WriteLine(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CATEGORY));
            string name = ConsoleHelper.GetStringInput($"\t{FormatHelper.ToTitleCase(StringConstants.NAME)}: ");
            Category catogory =  _categoryService.Find(item => item.Name.ToUpper() == name.ToUpper());
            if (catogory != null)
            {
                Console.WriteLine(string.Format(StringConstants.X_IS_EXIST_IN_LIST, StringConstants.CATEGORY));
                return;
            }
            _categoryService.Add(new Category(name));
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_ADDED_SUCCESSFULLY, StringConstants.CATEGORY));
        }

        public void Update()
        {
            DisplayAllItems();
            int catogoryId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_UPDATE, StringConstants.CATEGORY));

            Category catogory = _categoryService.GetById(catogoryId);
            if (catogory != null)
            {

                string newName = ConsoleHelper.GetStringInput(string.Format(StringConstants.INPUT_NAME_OF_X_NEW, StringConstants.CATEGORY));
                catogory.Name = newName;
                _categoryService.Update(catogory);
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND,StringConstants.CATEGORY));
            }
        }

        public void Delete()
        {
            DisplayAllItems();
            int catogoryId = ConsoleHelper.GetIntInput(string.Format(StringConstants.ENTER_THE_ID_OF_X_TO_DELETE, StringConstants.CATEGORY));
            if (!CanDeleteCategory(catogoryId))
            {
                Console.WriteLine(string.Format(StringConstants.CANNOT_DELETE_X_ASSOCIATED_Y, StringConstants.CATEGORY, StringConstants.PRODUCT));
                return;
            }
            _categoryService.Delete(catogoryId);
            Console.WriteLine(string.Format(StringConstants.X_HAS_BEEN_REMOVE, StringConstants.CATEGORY));
        }

        public Category FindById(int categoryId)
        {
            Category category = _categoryService.GetById(categoryId);
            return category;
        }

        public bool CanDeleteCategory(int categoryId)
        {
            LinkedList<Product> products = _productService.GetAllItems();
            Node<Product> product = products.Find(p => p.CategoryId == categoryId);
            if (product != null)
            {
                return false;
            }
            return true;
        }
    }
}
