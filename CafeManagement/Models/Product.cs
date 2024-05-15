namespace CafeManagement.Models
{
    public class Product
    {
        public int Id { get; set; }           // Mã sản phẩm
        public string Name { get; set; }      // Tên sản phẩm
        public int CategoryId { get; set; }   // Mã loại sản phẩm
        public double Price { get; set; }    // Giá sản phẩm

        public Product(string name, int categoryId, double price, int id = 0)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, CategoryID: {CategoryId}, Price: {Price}";
        }
    }
}
