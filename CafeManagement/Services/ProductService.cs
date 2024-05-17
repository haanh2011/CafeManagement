using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class ProductService
    {
        private List<Product> _products;
        private readonly string _filePath;

        public ProductService(string filePath)
        {
            _filePath = filePath;
            _products = DataManager.LoadProducts(_filePath);
        }
        public List<Product> GetAll()
        {
            return _products;
        }

        public void Display()
        {
            foreach (var product in _products)
            {
                Console.WriteLine($"Sản phẩm ID: {product.Id}, Tên: {product.Name}, Loại sản phẩm: {product.CategoryId}, Giá: {product.Price}");
            }
        }

        public void Add(Product product)
        {
            int maxId = _products.Count > 0 ? _products.Max(p => p.Id) : 0;
            product.Id = maxId + 1;
            _products.Add(product);
            DataManager.SaveProducts(_filePath, _products);
        }

        public void Update(Product updatedProduct)
        {
            var product = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (product.Equals(default(Product)))
            {
                Console.WriteLine("Không tìm thấy sản phẩm với mã số đó.");
            }
            else
            {
                product.Name = updatedProduct.Name;
                product.CategoryId = updatedProduct.CategoryId;
                product.Price = updatedProduct.Price;
                DataManager.SaveProducts(_filePath, _products);
                Console.WriteLine("Sản phẩm đã được cập nhật.");
            }
        }

        public void Delete(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product.Equals(default(Product)))
            {
                Console.WriteLine("Không tìm thấy sản phẩm với mã số đó.");
            }
            else
            {
                _products.Remove(product);
                DataManager.SaveProducts(_filePath, _products);
                Console.WriteLine("Sản phẩm đã được xóa.");
            }
        }

        public Product GetById(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }
    }
}
