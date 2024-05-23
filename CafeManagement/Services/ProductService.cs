using System;
using CafeManagement.Constants;
using CafeManagement.Manager;
using CafeManagement.Models;
using CafeManagement.Utilities;

namespace CafeManagement.Services
{
    public class ProductService
    {
        public LinkedList<Product> Products;
        private readonly string _filePath;

        /// <summary>
        /// Khởi tạo một đối tượng dịch vụ sản phẩm mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp lưu trữ sản phẩm.</param>
        public ProductService(string filePath)
        {
            _filePath = filePath;
            Products = DataManager.LoadProducts(_filePath);
        }

        /// <summary>
        /// Lấy danh sách tất cả sản phẩm.
        /// </summary>
        /// <returns>Danh sách tất cả sản phẩm.</returns>
        public void GetAllItems()
        {
            Products = DataManager.LoadProducts(_filePath);
        }

        /// <summary>
        /// Thêm một sản phẩm mới.
        /// </summary>
        /// <param name="product">Sản phẩm cần thêm.</param>
        /// <returns>Sản phẩm đã được thêm vào danh sách.</returns>
        public Product Add(Product product)
        {
            Product productMax = Products.Max(p => p.Id);
            int maxId = Products.Count > 0 ? productMax.Id : 0;
            product.Id = maxId + 1;
            Products.AddLast(product);
            DataManager.SaveProducts(_filePath, Products);
            return product;
        }

        /// <summary>
        /// Cập nhật thông tin một sản phẩm.
        /// </summary>
        /// <param name="updatedProduct">Sản phẩm cần cập nhật thông tin.</param>
        public void Update(Product updatedProduct)
        {
            Node<Product> product = Products.Find(p => p.Id == updatedProduct.Id);
            if (product != null)
            {
                product.Data.Name = updatedProduct.Name;
                product.Data.CategoryId = updatedProduct.CategoryId;
                product.Data.Price = updatedProduct.Price;
                DataManager.SaveProducts(_filePath, Products);
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.PRODUCT));
            }
        }

        /// <summary>
        /// Xóa một sản phẩm dựa trên ID.
        /// </summary>
        /// <param name="productId">ID của sản phẩm cần xóa.</param>
        public void Delete(int productId)
        {
            Node<Product> product = Products.Find(p => p.Id == productId);
            if (product != null)
            {
                Products.RemoveNode(product);
                DataManager.SaveProducts(_filePath, Products);
                Console.WriteLine("Sản phẩm đã được xóa.");
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_WITH_THE_ENTERED_ID_WAS_NOT_FOUND, StringConstants.PRODUCT));
            }
        }

        /// <summary>
        /// Lấy thông tin của một sản phẩm dựa trên ID.
        /// </summary>
        /// <param name="id">ID của sản phẩm cần lấy thông tin.</param>
        /// <returns>Sản phẩm thỏa mãn ID hoặc null nếu không tìm thấy.</returns>
        public Product GetById(int id)
        {
            return Products.Find(p => p.Id == id)?.Data;
        }
    }
}
