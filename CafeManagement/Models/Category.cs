namespace CafeManagement.Models
{
    public class Category
    {
        public int Id { get; set; }           // Mã loại sản phẩm
        public string Name { get; set; }      // Tên loại sản phẩm

        public Category(string name, int id = 0)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
