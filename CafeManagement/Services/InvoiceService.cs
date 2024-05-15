using System;
using System.Collections.Generic;
using System.Linq;
using CafeManagement.Manager;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class InvoiceService
    {
        private readonly string _filePath;
        private readonly List<Invoice> _invoices;

        public InvoiceService(string filePath)
        {
            _filePath = filePath;
            _invoices = DataManager.LoadInvoices(filePath);
        }

        public List<Invoice> GetAllItems()
        {
            return _invoices;
        }

        public int Create(Invoice invoice)
        {
            // Generate a unique invoice ID
            int maxId = _invoices.Count > 0 ? _invoices.Max(i => i.Id) : 0;
            invoice.Id = maxId + 1;

            // Add the invoice to the list
            _invoices.Add(invoice);

            // Save the invoices to file
            DataManager.SaveInvoices(_filePath, _invoices);
            return invoice.Id;
        }

        public void Update(Invoice updatedInvoice)
        {
            // Find the index of the invoice to update
            int index = _invoices.FindIndex(i => i.Id == updatedInvoice.Id);
            if (index != -1)
            {
                _invoices[index] = updatedInvoice;

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
            Invoice invoiceToDelete = _invoices.FirstOrDefault(i => i.Id == invoiceId);
            if (invoiceToDelete != null)
            {
                _invoices.Remove(invoiceToDelete);

                // Save the invoices to file
                DataManager.SaveInvoices(_filePath, _invoices);
            }
            else
            {
                throw new InvalidOperationException("Không tìm thấy hoá đơn.");
            }
        }

        public Invoice GetById(int id)
        {
            return _invoices.FirstOrDefault(i => i.Id == id);
        }
    }
}
