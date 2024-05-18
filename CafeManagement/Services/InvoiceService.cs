using System;
using CafeManagement.Constants;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class InvoiceService
    {
        private LinkedList<Invoice> _invoices;
        private readonly string _filePath;

        public InvoiceService(string filePath)
        {
            _filePath = filePath;
            _invoices = DataManager.LoadInvoices(filePath);
        }

        public LinkedList<Invoice> GetAllItems()
        {
            return _invoices;
        }

        public Invoice Add(Invoice invoice)
        {
            // Generate a unique invoice ID
            Invoice invoceMax = _invoices.Max(i => i.Id);
            int maxId = _invoices.Count > 0 ? invoceMax.Id : 0;
            invoice.Id = maxId + 1;

            // Add the invoice to the list
            _invoices.AddLast(invoice);

            // Save the invoices to file
            DataManager.SaveInvoices(_filePath, _invoices);
            return invoice;
        }

        public void Update(Invoice updatedInvoice)
        {
            // Find the index of the invoice to update
            Node<Invoice> invoice = _invoices.Find(obj => obj.Id == updatedInvoice.Id);
            if (invoice != null)
            {
                invoice.Data = updatedInvoice;

                // Save the invoices to file
                DataManager.SaveInvoices(_filePath, _invoices);
            }
            else
            {
                throw new InvalidOperationException("Không tìm thấy hoá đơn.");
            }
        }

        public void Delete(int invoiceId)
        {
            // Find the invoice to delete
            Node<Invoice> invoice = _invoices.Find(i => i.Id == invoiceId);
            if (invoice != null)
            {
                _invoices.RemoveNode(invoice);
                // Save the invoices to file
                DataManager.SaveInvoices(_filePath, _invoices);
                Console.WriteLine("Đã xoá hoá đơn thành công.");
            }
            else
            {
                Console.WriteLine(string.Format(StringConstants.X_NOT_FOUND, StringConstants.INVOICE));

            }
        }

        public Invoice GetById(int id)
        {
            return _invoices.Find(i => i.Id == id)?.Data;
        }
    }
}
