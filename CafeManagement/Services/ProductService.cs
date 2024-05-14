using System.Collections.Generic;
using System.Linq;
using CafeManagement.Data;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class ProductService
    {
        private readonly string _filePath;
        private readonly List<Product> _products;
        public ProductService(string filePath, List<Product> products)
        {
            _filePath = filePath;
            _products = products;
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            DataManager.SaveProducts(_filePath, _products);
        }

        public void UpdateProduct(Product updatedProduct)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].Id == updatedProduct.Id)
                {
                    _products[i] = updatedProduct;
                    break;
                }
            }
            DataManager.SaveProducts(_filePath, _products);
        }

        public void DeleteProduct(int productId)
        {
            _products.RemoveAll(p => p.Id == productId);
            DataManager.SaveProducts(_filePath, _products);
        }
    }
}
