namespace CafeManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }                // Mã khách hàng
        public string Name { get; set; }           // Tên khách hàng
        public string Email { get; set; }          // Email khách hàng
        public string PhoneNumber { get; set; }    // Số điện thoại khách hàng
        public int Points { get; set; }            // Điểm tích lũy

        public Customer(string name, string email, string phoneNumber, int id= 0, int points = 0)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Points = points;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, PhoneNumber: {PhoneNumber}, Points: {Points}";
        }
    }
}
