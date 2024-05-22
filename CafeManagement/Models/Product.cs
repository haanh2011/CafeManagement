using CafeManagement.Manager;
using CafeManagement.Utilities;

namespace CafeManagement.Models
{
    /// <summary>
    /// Đại diện cho một sản phẩm trong cửa hàng.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Mã sản phẩm.
        /// </summary>
        public int Id { get; set; }           // Mã sản phẩm

        /// <summary>
        /// Tên sản phẩm.
        /// </summary>
        public string Name { get; set; }      // Tên sản phẩm

        /// <summary>
        /// Mã loại sản phẩm.
        /// </summary>
        public int CategoryId { get; set; }   // Mã loại sản phẩm

        /// <summary>
        /// Giá sản phẩm.
        /// </summary>
        public double Price { get; set; }    // Giá sản phẩm

        /// <summary>
        /// Khởi tạo một đối tượng Product mới.
        /// </summary>
        /// <param name="name">Tên sản phẩm.</param>
        /// <param name="categoryId">Mã loại sản phẩm.</param>
        /// <param name="price">Giá sản phẩm.</param>
        /// <param name="id">Mã sản phẩm (mặc định là 0).</param>
        public Product(string name, int categoryId, double price, int id = 0)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }

        /// <summary>
        /// Chuyển đổi đối tượng Product thành một chuỗi để xuất trong table.
        /// </summary>
        /// <returns>Chuỗi biểu diễn thông tin của sản phẩm.</returns>
        public override string ToString()
        {
            // Lấy danh sách loại sản phẩm đã lưu
            LinkedList<Category> categories = DataManager.LoadCategories("Data/CategoryData.txt");
            Category category = categories.Find(c => c.Id == CategoryId)?.Data;
            return $"| {Id,5} | {Name,-25} | {FormatHelper.FormatToVND(Price),15} |";
        }
    }
}
