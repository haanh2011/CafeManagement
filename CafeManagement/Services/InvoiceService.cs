using System.Collections.Generic;
using CafeManagement.Data;
using CafeManagement.Models;

namespace CafeManagement.Services
{
    public class InvoiceService
    {
        private readonly string _filePath;
        private readonly List<Invoice> _invoices;

        public InvoiceService(string filePath, List<Invoice> invoices)
        {
            _filePath = filePath;
            _invoices = invoices;
        }

        public void AddInvoice(Invoice invoice)
        {
            var invoices = DataManager.LoadInvoices(_filePath);
            invoices.Add(invoice);
            DataManager.SaveInvoices(_filePath, invoices);
        }

        public void UpdateInvoice(Invoice updatedInvoice)
        {
            var invoices = DataManager.LoadInvoices(_filePath);
            for (int i = 0; i < invoices.Count; i++)
            {
                if (invoices[i].Id == updatedInvoice.Id)
                {
                    invoices[i] = updatedInvoice;
                    break;
                }
            }
            DataManager.SaveInvoices(_filePath, invoices);
        }

        public void DeleteInvoice(int invoiceId)
        {
            var invoices = DataManager.LoadInvoices(_filePath);
            invoices.RemoveAll(i => i.Id == invoiceId);
            DataManager.SaveInvoices(_filePath, invoices);
        }
    }
}
