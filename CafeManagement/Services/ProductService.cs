using System;
using CafeManagement.Constants;
using CafeManagement.Manager;
using CafeManagement.Models;
using CafeManagement.Utilities;

namespace CafeManagement.Services
{
    public class ProductService
    {
        private LinkedList<Product> _products;
        private readonly string _filePath;

        public ProductService(string filePath)
        {
            _filePath = filePath;
            _products = DataManager.LoadProducts(_filePath);
        }
        public LinkedList<Product> GetAllItems()
        {
            return _products;
        }

        public Product Add(Product product)
        {
            Product productMax = _products.Max(p => p.Id);
            int maxId = _products.Count > 0 ? productMax.Id : 0;
            product.Id = maxId + 1;
            _products.AddLast(product);
            DataManager.SaveProducts(_filePath, _products);
            return product;
        }

        public void Update(Product updatedProduct)
        {
            Node<Product> product = _products.Find(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Data.Name = updatedProduct.Name;
                product.Data.CategoryId = updatedProduct.CategoryId;
                product.Data.Price = updatedProduct.Price;
                DataManager.SaveProducts(_filePath, _products);
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.PRODUCT));
            }
        }

        public void Delete(int productId)
        {
            Node<Product> product = _products.Find(p => p.Id == productId);
            if (product != null)
            {
                _products.RemoveNode(product);
                DataManager.SaveProducts(_filePath, _products);
                Console.WriteLine("Sản phẩm đã được xóa.");
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.PRODUCT));
            }
        }

        public Product GetById(int id)
        {
            return _products.Find(p => p.Id == id)?.Data;
        }
    }
}
