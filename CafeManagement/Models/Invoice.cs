using System;

namespace CafeManagement.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }

        public Invoice(int id, int orderId, double total, DateTime date)
        {
            Id = id;
            OrderId = orderId;
            Total = total;
            Date = date;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Order ID: {OrderId}, Total: {Total}, Date: {Date.ToShortDateString()}";
        }
    }
}
