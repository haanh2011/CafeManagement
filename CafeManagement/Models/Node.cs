namespace CafeManagement.Models
{
    /// <summary>
    /// Đại diện cho một nút trong danh sách liên kết đơn.
    /// </summary>
    public class Node<T>
    {
        public T Data; // Dữ liệu của nút
        public Node<T> Next; // Tham chiếu đến nút tiếp theo trong danh sách

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp Node với dữ liệu được chỉ định.
        /// </summary>
        /// <param name="d">Dữ liệu cần lưu trữ trong nút.</param>
        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }
}
