using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeManagement.Models
{
    /// <summary>
    /// Đại diện cho một danh sách liên kết đôi.
    /// </summary>
    public class LinkedList<T>
    {
        private Node<T> _first; // Tham chiếu đến nút đầu tiên trong danh sách
        private Node<T> _last; // Tham chiếu đến nút cuối cùng trong danh sách
        private int _size; // Số lượng nút trong danh sách

        /// <summary>
        /// Lấy số lượng phần tử trong danh sách liên kết đôi.
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Lấy nút đầu tiên trong danh sách liên kết đôi.
        /// </summary>
        public Node<T> First
        {
            get { return _first; }
        }

        /// <summary>
        /// Lấy nút cuối cùng trong danh sách liên kết đôi.
        /// </summary>
        public Node<T> Last
        {
            get { return _last; }
        }

        /// <summary>
        /// Khởi tạo một thể hiện mới của lớp LinkedList.
        /// </summary>
        public LinkedList()
        {
            _first = null;
            _last = null;
            _size = 0;
        }

        /// <summary>
        /// In ra toàn bộ danh sách liên kết trên màn hình console.
        /// </summary>
        public void PrintList()
        {
            // Khởi tạo một node bắt đầu từ node đầu tiên của danh sách liên kết
            Node<T> node = _first;
            // Duyệt qua danh sách liên kết và in ra màn hình console
            Console.Write("{");

            while (node != null)
            {
                // In giá trị của node hiện tại ra màn hình console
                Console.Write("  " + node.Data);
                // Di chuyển đến node kế tiếp trong danh sách liên kết
                node = node.Next;
            }
            Console.WriteLine("  }");
        }

        public bool IsEqual()
        {
            if (_size != Count)
            {
                return false;
            }

            T[] arr1 = new T[_size];
            Node<T> current = _first;
            for (int i = 0; i < _size; i++)
            {
                arr1[i] = current.Data;
                current = current.Next;
            }

            T[] arr2 = new T[Count];
            current = First;
            for (int i = 0; i < Count; i++)
            {
                arr2[i] = current.Data;
                current = current.Next;
            }

            Array.Sort(arr1);
            Array.Sort(arr2);

            return Enumerable.SequenceEqual(arr1, arr2);
        }

        public Node<T> FindNodeByValue(T data)
        {
            // Bắt đầu tìm kiếm từ đầu danh sách liên kết
            Node<T> current = _first;
            while (current != null)
            {
                // Nếu tìm thấy giá trị dữ liệu, trả về nút chứa giá trị đó
                if (current.Data.Equals(data))
                {
                    return current;
                }
                // Di chuyển đến nút kế tiếp trong danh sách liên kết
                current = current.Next;
            }
            // Trả về null nếu không tìm thấy giá trị dữ liệu trong danh sách
            return null;
        }

        public Node<T> Find(Func<T, bool> predicate)
        {
            // Bắt đầu tìm kiếm từ đầu danh sách liên kết
            Node<T> current = _first;
            while (current != null)
            {
                // So sánh giá trị của thuộc tính được chọn với giá trị được cung cấp
                if (predicate(current.Data))
                {
                    return current;
                }
                // Di chuyển đến nút kế tiếp trong danh sách liên kết
                current = current.Next;
            }
            // Trả về null nếu không tìm thấy nút
            return null;
        }

        public Node<T> FindNodeAtIndex(int index)
        {
            // Kiểm tra xem chỉ mục có hợp lệ không
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Chỉ mục vượt ra khỏi phạm vi của danh sách.");
            }

            // Tìm nút ở vị trí chỉ mục
            Node<T> current = _first;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current;
        }

        public Node<T> FindLast(T data)
        {
            // Kiểm tra danh sách liên kết có trống không
            if (_first == null)
            {
                return null; // Danh sách trống, trả về null ngay lập tức
            }

            // Bắt đầu tìm kiếm từ đầu danh sách liên kết
            Node<T> current = _first;
            Node<T> lastNode = null;
            while (current != null)
            {
                // Nếu tìm thấy giá trị dữ liệu, cập nhật nút cuối cùng thỏa mãn điều kiện
                if (current.Data.Equals(data))
                {
                    lastNode = current;
                }
                // Di chuyển đến nút kế tiếp trong danh sách liên kết
                current = current.Next;
            }

            // Trả về nút cuối cùng thỏa mãn điều kiện hoặc null nếu không tìm thấy
            return lastNode;
        }

        public bool Contains(T data)
        {
            Node<T> current = _first;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void AddFirst(T newData)
        {
            Node<T> newNode = new Node<T>(newData);
            if (_first == null)
            {
                // Nếu danh sách trống, nút mới sẽ là nút đầu tiên và cũng là nút cuối cùng
                _first = newNode;
                _last = _first;
            }
            else
            {
                // Nếu danh sách không trống, thêm vào trước nút đầu tiên
                newNode.Next = _first;
                _first = newNode;
            }
            _size++;
        }

        public void AddLast(T newData)
        {
            if (_first == null)
            {
                // Nếu danh sách trống, thêm vào đầu danh sách
                AddFirst(newData);
            }
            else
            {
                // Nếu danh sách không trống, thêm vào sau nút cuối cùng
                Node<T> newNode = new Node<T>(newData);
                _last.Next = newNode;
                _last = newNode;
                _size++;
            }
        }

        public void AddAfter(Node<T> pre, T newData)
        {
            if (pre != null)
            {
                Node<T> newNode = new Node<T>(newData);
                newNode.Next = pre.Next;
                pre.Next = newNode;
                if (pre == _last)
                {
                    _last = newNode;
                }
                _size++;
            }
        }

        public void AddBefore(Node<T> node, T newData)
        {
            // Kiểm tra nếu tham số node là null
            if (node != null)
            {

                // Tạo một nút mới với dữ liệu mới
                Node<T> newNode = new Node<T>(newData);

                // Nếu nút được chỉ định là nút đầu tiên trong danh sách
                if (node == _first)
                {
                    newNode.Next = _first; // Nút mới trỏ đến nút đầu tiên hiện tại
                    _first = newNode; // Nút mới trở thành nút đầu tiên trong danh sách
                }
                else
                {
                    // Duyệt qua danh sách để tìm nút trước nút được chỉ định
                    Node<T> current = _first;
                    while (current != null && current.Next != node)
                    {
                        current = current.Next;
                    }

                    // Nếu không tìm thấy nút trong danh sách
                    if (current == null)
                    {
                        throw new InvalidOperationException("Node not found in the list.");
                    }

                    // Thiết lập tham chiếu của nút mới để trỏ đến nút sau nút hiện tại
                    newNode.Next = current.Next;
                    // Thiết lập tham chiếu của nút hiện tại để trỏ đến nút mới
                    current.Next = newNode;
                }

                // Tăng kích thước của danh sách lên sau khi thêm nút mới
                _size++;
            }
        }

        /// <summary>
        /// Thêm nhiều phần tử vào cuối danh sách liên kết.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của các phần tử trong danh sách liên kết.</typeparam>
        /// <param name="items">Danh sách các phần tử cần thêm vào danh sách liên kết.</param>
        /// 
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                AddLast(item);
            }
        }

        /// <summary>
        /// Xóa nút đầu tiên khỏi danh sách liên kết.
        /// </summary>
        public void RemoveFirst()
        {
            if (_first == null) return;

            _first = _first.Next;
            if (_first == null)
            {
                _last = null;
            }
            _size--;
        }

        /// <summary>
        /// Xóa nút cuối cùng khỏi danh sách liên kết.
        /// </summary>
        public void RemoveLast()
        {
            if (_first == null) return;

            if (_first == _last)
            {
                _first = null;
                _last = null;
            }
            else
            {
                Node<T> current = _first;
                while (current.Next != _last)
                {
                    current = current.Next;
                }
                current.Next = null;
                _last = current;
            }
            _size--;
        }

        /// <summary>
        /// Xóa nút khỏi danh sách liên kết.
        /// </summary>
        /// <param name="node">Nút cần xóa.</param>
        public void RemoveNode(Node<T> node)
        {
            if (node == null) return;

            // Nếu nút cần xóa là nút đầu tiên
            if (node == _first)
            {
                RemoveFirst();
                return;
            }

            // Nếu nút cần xóa là nút cuối cùng
            if (node == _last)
            {
                RemoveLast();
                return;
            }

            // Tìm nút trước nút cần xóa
            Node<T> current = _first;
            while (current != null && current.Next != node)
            {
                current = current.Next;
            }

            // Ngắt liên kết với nút cần xóa nếu tìm thấy nút trước đó
            if (current != null)
            {
                current.Next = node.Next;
                _size--;
            }
        }

        /// <summary>
        /// Xóa một nút chứa dữ liệu đã cho khỏi danh sách liên kết.
        /// </summary>
        /// <param name="data">Dữ liệu của nút cần xóa.</param>
        public void RemoveByValue(T data)
        {
            Node<T> node = FindNodeByValue(data);
            if (node != null)
            {
                RemoveNode(node);
            }
        }

        /// <summary>
        /// Xóa một nút chứa dữ liệu đã cho khỏi danh sách liên kết.
        /// </summary>
        /// <param name="data">Dữ liệu của nút cần xóa.</param>
        public void RemoveAtIndex(int index)
        {
            Node<T> node = FindNodeAtIndex(index);
            if (node != null)
            {
                RemoveNode(node);
            }
        }

        /// <summary>
        /// Xóa nút sau một nút đã cho khỏi danh sách liên kết.
        /// </summary>
        /// <param name="pre">Nút trước nút cần xóa.</param>
        public void RemoveAfter(Node<T> pre)
        {
            if (pre != null && pre.Next != null)
            {
                RemoveNode(pre.Next);
            }
        }

        /// <summary>
        /// Trả về giá trị của phần tử ở vị trí index trong danh sách liên kết.
        /// </summary>
        /// <param name="index">Vị trí của phần tử cần truy xuất.</param>
        /// <returns>Giá trị của phần tử ở vị trí index.</returns>
        public T DatatElementOfIndex(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Chỉ số nằm ngoài phạm vi.");
            }
            Node<T> current;
            if (index == _size - 1)
            {
                current = _last;
                return current.Data;
            }
            else
            {
                current = _first;
                if (index == 0)
                {
                    return current.Data;
                }
                else
                {
                    for (int i = 1; i <= index; i++)
                    {
                        current = current.Next;
                    }
                }
            }
            return current.Data;
        }

        /// <summary>
        /// Update node by value
        /// </summary>
        /// <param name="oldValue">giá trị cũ</param>
        /// <param name="newValue">Giá trị mói</param>
        public void UpdateNodeByValue(T oldValue, T newValue)
        {
            Node<T> current = _first;
            bool isUpdate = false;
            if (_first.Data.Equals(oldValue))
            {
                RemoveFirst();
                AddFirst(newValue);
            }
            if (_last.Data.Equals(oldValue))
            {
                RemoveLast();
                AddLast(newValue);
            }

            while (current.Next != null)
            {
                if (current.Next.Data.Equals(oldValue))
                {
                    RemoveAfter(current);
                    AddAfter(current, newValue);
                }
                current = current.Next;
            }
            if (isUpdate)
            {
                Console.WriteLine("Đã update data thành công");
            }
            else
            {
                Console.WriteLine($"Update không thành công. Không tồn tại node có giá trị '{oldValue}'");
            }
        }

        // Phương thức chuyển đổi LinkedList<T> thành danh sách List<T>
        public List<T> ToList()
        {
            List<T> list = new List<T>();
            Node<T> current = _first;
            while (current != null)
            {
                list.Add(current.Data);
                current = current.Next;
            }
            return list;
        }

        public T Min(Func<T, int> selector)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Danh sách liên kết trống.");
            }

            T min = _first.Data;
            int minValue = selector(min);
            Node<T> current = _first.Next;
            while (current != null)
            {
                int currentValue = selector(current.Data);
                if (currentValue < minValue)
                {
                    min = current.Data;
                    minValue = currentValue;
                }
                current = current.Next;
            }
            return min;
        }

        public T Max(Func<T, int> selector)
        {
            if (_size == 0)
            {
                return default(T);
            }
            T max = _first.Data;
            int maxValue = selector(max);
            Node<T> current = _first.Next;
            while (current != null)
            {
                int currentValue = selector(current.Data);
                if (currentValue > maxValue)
                {
                    max = current.Data;
                    maxValue = currentValue;
                }
                current = current.Next;
            }
            return max;
        }
    }
}