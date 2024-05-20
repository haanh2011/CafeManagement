using CafeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Đại diện cho một danh sách liên kết đôi.
/// </summary>
/// <typeparam name="T">Kiểu dữ liệu của các phần tử trong danh sách liên kết.</typeparam>
public class LinkedList<T>
{
    // Khai báo các biến thành viên

    /// <summary>
    /// Tham chiếu đến nút đầu tiên trong danh sách.
    /// </summary>
    private Node<T> _first;

    /// <summary>
    /// Tham chiếu đến nút cuối cùng trong danh sách.
    /// </summary>
    private Node<T> _last;

    /// <summary>
    /// Số lượng nút trong danh sách.
    /// </summary>
    private int _size;

    // Các thuộc tính

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

    // Constructor
    /// <summary>
    /// Khởi tạo một thể hiện mới của lớp LinkedList.
    /// </summary>
    public LinkedList()
    {
        _first = null;
        _last = null;
        _size = 0;
    }

    // Các phương thức

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

    /// <summary>
    /// Kiểm tra xem danh sách liên kết hiện tại có bằng với một danh sách khác không.
    /// </summary>
    /// <returns>True nếu các danh sách giống nhau, ngược lại trả về False.</returns>
    public bool IsEqual()
    {
        // Kiểm tra xem kích thước của danh sách hiện tại có khác với kích thước của danh sách khác không
        if (_size != Count)
        {
            return false; // Trả về false nếu kích thước không giống nhau
        }

        // Khởi tạo mảng arr1 để lưu trữ dữ liệu từ danh sách hiện tại
        T[] arr1 = new T[_size];
        Node<T> current = _first;
        for (int i = 0; i < _size; i++)
        {
            arr1[i] = current.Data; // Lưu trữ dữ liệu của nút hiện tại vào mảng arr1
            current = current.Next; // Di chuyển đến nút kế tiếp trong danh sách liên kết
        }

        // Khởi tạo mảng arr2 để lưu trữ dữ liệu từ danh sách khác
        T[] arr2 = new T[Count];
        current = First;
        for (int i = 0; i < Count; i++)
        {
            arr2[i] = current.Data; // Lưu trữ dữ liệu của nút hiện tại vào mảng arr2
            current = current.Next; // Di chuyển đến nút kế tiếp trong danh sách khác
        }

        // Sắp xếp các mảng để so sánh dễ dàng hơn
        Array.Sort(arr1);
        Array.Sort(arr2);

        // So sánh hai mảng để kiểm tra xem chúng có giống nhau không
        return Enumerable.SequenceEqual(arr1, arr2);
    }

    /// <summary>
    /// Tìm kiếm và trả về nút chứa giá trị dữ liệu đã cho.
    /// </summary>
    /// <param name="data">Giá trị dữ liệu cần tìm.</param>
    /// <returns>Nút chứa giá trị dữ liệu nếu tìm thấy, ngược lại trả về null.</returns>
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

    /// <summary>
    /// Tìm kiếm và trả về nút đầu tiên trong danh sách liên kết mà thuộc tính được chọn của dữ liệu thỏa mãn một điều kiện nhất định.
    /// </summary>
    /// <param name="predicate">Điều kiện tìm kiếm dựa trên thuộc tính của dữ liệu.</param>
    /// <returns>Nút đầu tiên trong danh sách liên kết thỏa mãn điều kiện, nếu không tìm thấy trả về null.</returns>
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

    /// <summary>
    /// Tìm và trả về nút ở vị trí chỉ mục trong danh sách liên kết.
    /// </summary>
    /// <param name="index">Chỉ mục của nút cần tìm.</param>
    /// <returns>Nút ở vị trí chỉ mục đã cho.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Ném ra khi chỉ mục nằm ngoài phạm vi của danh sách.</exception>
    public Node<T> FindNodeAtIndex(int index)
    {
        // Kiểm tra xem chỉ mục có hợp lệ không
        if (index < 0 || index >= _size)
        {
            Console.WriteLine("Chỉ mục vượt ra khỏi phạm vi của danh sách.");
        }

        // Tìm nút ở vị trí chỉ mục
        Node<T> current = _first;
        for (int i = 0; i < index; i++)
        {
            current = current.Next;
        }

        return current;
    }

    /// <summary>
    /// Tìm và trả về nút cuối cùng trong danh sách liên kết chứa dữ liệu đã cho.
    /// </summary>
    /// <param name="data">Dữ liệu cần tìm.</param>
    /// <returns>Nút cuối cùng chứa dữ liệu đã cho, nếu không tìm thấy trả về null.</returns>
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

    /// <summary>
    /// Kiểm tra xem danh sách liên kết có chứa một phần tử cụ thể không.
    /// </summary>
    /// <param name="data">Phần tử cần kiểm tra.</param>
    /// <returns>True nếu danh sách chứa phần tử cụ thể, ngược lại trả về False.</returns>
    public bool Contains(T data)
    {
        // Bắt đầu từ nút đầu tiên của danh sách
        Node<T> current = _first;
        while (current != null)
        {
            // So sánh dữ liệu của nút hiện tại với phần tử cần kiểm tra
            if (current.Data.Equals(data))
            {
                return true; // Trả về true nếu phần tử được tìm thấy trong danh sách
            }
            current = current.Next; // Di chuyển đến nút kế tiếp trong danh sách liên kết
        }
        return false; // Trả về false nếu không tìm thấy phần tử trong danh sách
    }

    /// <summary>
    /// Thêm một phần tử vào đầu danh sách liên kết.
    /// </summary>
    /// <param name="newData">Dữ liệu mới cần thêm vào.</param>
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

    /// <summary>
    /// Thêm một phần tử vào cuối danh sách liên kết.
    /// </summary>
    /// <param name="newData">Dữ liệu mới cần thêm vào.</param>
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

    /// <summary>
    /// Thêm một phần tử mới vào sau một nút đã cho trong danh sách liên kết.
    /// </summary>
    /// <param name="pre">Nút trước đó mà phần tử mới sẽ được thêm vào sau đó.</param>
    /// <param name="newData">Dữ liệu của phần tử mới cần thêm vào.</param>
    public void AddAfter(Node<T> pre, T newData)
    {
        // Kiểm tra xem nút trước đó đã được xác định chưa
        if (pre != null)
        {
            // Tạo một nút mới chứa dữ liệu mới
            Node<T> newNode = new Node<T>(newData);

            // Liên kết nút mới với nút sau của nút trước đó
            newNode.Next = pre.Next;

            // Liên kết nút trước với nút mới
            pre.Next = newNode;

            // Nếu nút trước đó là nút cuối cùng, cập nhật _last thành nút mới
            if (pre == _last)
            {
                _last = newNode;
            }

            // Tăng kích thước của danh sách liên kết
            _size++;
        }
    }

    /// <summary>
    /// Thêm một phần tử vào trước một nút đã cho trong danh sách liên kết.
    /// </summary>
    /// <param name="node">Nút trước đó phần tử mới sẽ được thêm vào.</param>
    /// <param name="newData">Dữ liệu mới cần thêm vào.</param>
    /// <exception cref="InvalidOperationException">Ném ra khi nút không được tìm thấy trong danh sách.</exception>
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
    /// Thêm một danh sách các phần tử vào cuối của danh sách liên kết.
    /// </summary>
    /// <param name="items">Danh sách các phần tử cần thêm vào danh sách liên kết.</param>
    public void AddRange(IEnumerable<T> items)
    {
        // Duyệt qua từng phần tử trong danh sách items
        foreach (var item in items)
        {
            // Thêm phần tử vào cuối danh sách liên kết bằng phương thức AddLast
            AddLast(item);
        }
    }

    /// <summary>
    /// Xóa nút đầu tiên khỏi danh sách liên kết.
    /// </summary>
    public void RemoveFirst()
    {
        // Kiểm tra xem danh sách có rỗng không
        if (_first == null) return;

        // Cập nhật nút đầu tiên của danh sách liên kết thành nút tiếp theo của nút hiện tại
        _first = _first.Next;

        // Nếu danh sách chỉ có một phần tử và nó đã được xóa
        if (_first == null)
        {
            _last = null; // Cập nhật nút cuối cùng thành null
        }
        _size--; // Giảm kích thước của danh sách liên kết
    }

    /// <summary>
    /// Xóa nút cuối cùng khỏi danh sách liên kết.
    /// </summary>
    public void RemoveLast()
    {
        // Kiểm tra xem danh sách có rỗng không
        if (_first == null) return;

        // Nếu danh sách chỉ có một phần tử
        if (_first == _last)
        {
            _first = null; // Xóa nút đầu tiên
            _last = null; // Xóa nút cuối cùng
        }
        else
        {
            Node<T> current = _first;
            while (current.Next != _last)
            {
                current = current.Next;
            }
            current.Next = null; // Ngắt liên kết với nút cuối cùng hiện tại
            _last = current; // Cập nhật nút cuối cùng thành nút trước đó
        }
        _size--; // Giảm kích thước của danh sách liên kết
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
        // Tìm nút chứa dữ liệu cần xóa
        Node<T> node = FindNodeByValue(data);
        // Nếu tìm thấy nút chứa dữ liệu
        if (node != null)
        {
            RemoveNode(node); // Gọi phương thức RemoveNode để xóa nút đó khỏi danh sách liên kết
        }
    }

    /// <summary>
    /// Xóa nút ở vị trí chỉ mục đã cho khỏi danh sách liên kết.
    /// </summary>
    /// <param name="index">Chỉ mục của nút cần xóa.</param>
    public void RemoveAtIndex(int index)
    {
        // Tìm nút ở vị trí chỉ mục đã cho
        Node<T> node = FindNodeAtIndex(index);
        // Nếu tìm thấy nút
        if (node != null)
        {
            RemoveNode(node); // Gọi phương thức RemoveNode để xóa nút đó khỏi danh sách liên kết
        }
    }

    /// <summary>
    /// Xóa nút sau một nút đã cho khỏi danh sách liên kết.
    /// </summary>
    /// <param name="pre">Nút trước nút cần xóa.</param>
    public void RemoveAfter(Node<T> pre)
    {
        // Kiểm tra xem nút trước đã cho và nút sau nút đó có tồn tại không
        if (pre != null && pre.Next != null)
        {
            RemoveNode(pre.Next); // Gọi phương thức RemoveNode để xóa nút sau nút đã cho khỏi danh sách liên kết
        }
    }
    
    /// <summary>
     /// Lấy giá trị của phần tử tại chỉ mục được chỉ định trong danh sách liên kết.
     /// </summary>
     /// <param name="index">Chỉ mục của phần tử cần lấy.</param>
     /// <returns>Giá trị của phần tử tại chỉ mục được chỉ định.</returns>
    public T DataElementOfIndex(int index)
    {
        // Kiểm tra xem chỉ mục có vượt ra khỏi phạm vi không
        if (index < 0 || index >= _size)
        {
            Console.WriteLine("Chỉ số nằm ngoài phạm vi.");
        }

        Node<T> current;

        // Lấy giá trị của phần tử tại chỉ mục được chỉ định
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
                // Duyệt qua danh sách để tìm nút tại chỉ mục được chỉ định
                for (int i = 1; i <= index; i++)
                {
                    current = current.Next;
                }
            }
        }
        return current.Data;
    }

    /// <summary>
    /// Cập nhật giá trị của nút chứa dữ liệu cũ thành giá trị mới.
    /// </summary>
    /// <param name="oldValue">Giá trị cũ của nút cần cập nhật.</param>
    /// <param name="newValue">Giá trị mới của nút.</param>
    /// <summary>
    /// Cập nhật giá trị của nút chứa giá trị cũ thành giá trị mới.
    /// </summary>
    /// <param name="oldValue">Giá trị cũ cần được cập nhật.</param>
    /// <param name="newValue">Giá trị mới.</param>
    public void UpdateNodeByValue(T oldValue, T newValue)
    {
        Node<T> current = _first;
        bool isUpdate = false;

        // Cập nhật giá trị của nút đầu tiên nếu có
        if (_first.Data.Equals(oldValue))
        {
            RemoveFirst();
            AddFirst(newValue);
            isUpdate = true;
        }

        // Cập nhật giá trị của nút cuối cùng nếu có
        if (_last.Data.Equals(oldValue))
        {
            RemoveLast();
            AddLast(newValue);
            isUpdate = true;
        }

        // Duyệt qua danh sách để cập nhật giá trị của các nút khác
        while (current.Next != null)
        {
            if (current.Next.Data.Equals(oldValue))
            {
                RemoveAfter(current);
                AddAfter(current, newValue);
                isUpdate = true;
            }
            current = current.Next;
        }

        // Hiển thị thông báo cập nhật
        if (isUpdate)
        {
            Console.WriteLine("Đã cập nhật dữ liệu thành công");
        }
        else
        {
            Console.WriteLine($"Cập nhật không thành công. Không tìm thấy nút chứa giá trị '{oldValue}'");
        }
    }

    /// <summary>
    /// Chuyển đổi danh sách liên kết thành danh sách List.
    /// </summary>
    /// <returns>Danh sách List chứa các phần tử từ danh sách liên kết.</returns>
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

    /// <summary>
    /// Trả về phần tử có giá trị nhỏ nhất dựa trên một selector được chỉ định.
    /// </summary>
    /// <param name="selector">Phương thức để chọn giá trị cần so sánh.</param>
    /// <returns>Phần tử có giá trị nhỏ nhất.</returns>
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

    /// <summary>
    /// Tìm phần tử có giá trị lớn nhất trong danh sách liên kết dựa trên một selector được chỉ định.
    /// </summary>
    /// <param name="selector">Delegate xác định giá trị được sử dụng để so sánh các phần tử.</param>
    /// <returns>Phần tử có giá trị lớn nhất.</returns>
    public T Max(Func<T, int> selector)
    {
        // Kiểm tra xem danh sách liên kết có trống không
        if (_size == 0)
        {
            return default(T); // Trả về giá trị mặc định nếu danh sách trống
        }

        // Khởi tạo biến lưu giá trị lớn nhất và giá trị tương ứng
        T max = _first.Data; // Giả sử phần tử đầu tiên có giá trị lớn nhất ban đầu
        int maxValue = selector(max); // Lấy giá trị của phần tử đầu tiên

        // Duyệt qua từng phần tử trong danh sách liên kết để tìm giá trị lớn nhất
        Node<T> current = _first.Next; // Bắt đầu từ phần tử thứ hai
        while (current != null)
        {
            // Tính toán giá trị cho phần tử hiện tại sử dụng selector
            int currentValue = selector(current.Data);

            // So sánh giá trị hiện tại với giá trị lớn nhất đã biết
            if (currentValue > maxValue)
            {
                // Nếu giá trị hiện tại lớn hơn, cập nhật giá trị lớn nhất và phần tử tương ứng
                max = current.Data;
                maxValue = currentValue;
            }

            // Di chuyển đến phần tử kế tiếp trong danh sách liên kết
            current = current.Next;
        }

        // Trả về phần tử có giá trị lớn nhất đã tìm được
        return max;
    }
}