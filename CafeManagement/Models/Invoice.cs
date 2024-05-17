using System;

namespace CafeManagement.Models
{
    public struct Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }

        public Invoice(int id, int orderId, DateTime date)
        {
            Id = id;
            OrderId = orderId;
            Date = date;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Order ID: {OrderId}, Date: {Date.ToShortDateString()}";
        }
    }
}
