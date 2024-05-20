namespace CafeManagement.Models
{
    /// <summary>
    /// Định nghĩa một loại sản phẩm trong quản lý quán cà phê.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Mã số duy nhất của loại sản phẩm.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên của loại sản phẩm.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp Category với tên và mã số cho trước.
        /// </summary>
        /// <param name="name">Tên của loại sản phẩm.</param>
        /// <param name="id">Mã số của loại sản phẩm (mặc định là 0 nếu không có giá trị được cung cấp).</param>
        public Category(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Trả về một chuỗi biểu diễn của đối tượng Category.
        /// </summary>
        /// <returns>Chuỗi biểu diễn của đối tượng Category.</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Tên: {Name}";
        }
    }
}
