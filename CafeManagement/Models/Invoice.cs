using CafeManagement.Constants;
using System;

namespace CafeManagement.Models
{
    public class Invoice
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
            return $"| {Id,5} | {OrderId,15} | {Date.ToString(StringConstants.FORMAT_DATETIME),-20} |";
        }
    }
}
