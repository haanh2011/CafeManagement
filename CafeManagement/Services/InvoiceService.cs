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

        /// <summary>
        /// Khởi tạo một đối tượng dịch vụ hóa đơn mới.
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp lưu trữ hóa đơn.</param>
        public InvoiceService(string filePath)
        {
            _filePath = filePath;
            _invoices = DataManager.LoadInvoices(filePath);
        }

        /// <summary>
        /// Lấy danh sách tất cả hóa đơn.
        /// </summary>
        /// <returns>Danh sách tất cả hóa đơn.</returns>
        public LinkedList<Invoice> GetAllItems()
        {
            return _invoices;
        }

        /// <summary>
        /// Thêm một hóa đơn mới.
        /// </summary>
        /// <param name="invoice">Hóa đơn cần thêm.</param>
        /// <returns>Hóa đơn đã được thêm vào danh sách.</returns>
        public Invoice Add(Invoice invoice)
        {
            // Tạo một ID hóa đơn duy nhất
            Invoice invoceMax = _invoices.Max(i => i.Id);
            int maxId = _invoices.Count > 0 ? invoceMax.Id : 0;
            invoice.Id = maxId + 1;

            // Thêm hóa đơn vào danh sách
            _invoices.AddLast(invoice);

            // Lưu danh sách hóa đơn vào tệp
            DataManager.SaveInvoices(_filePath, _invoices);
            return invoice;
        }

        /// <summary>
        /// Cập nhật thông tin một hóa đơn.
        /// </summary>
        /// <param name="updatedInvoice">Hóa đơn cần cập nhật thông tin.</param>
        public void Update(Invoice updatedInvoice)
        {
            // Tìm vị trí của hóa đơn cần cập nhật
            Node<Invoice> invoice = _invoices.Find(obj => obj.Id == updatedInvoice.Id);
            invoice.Data = updatedInvoice;

            // Lưu danh sách hóa đơn vào tệp
            DataManager.SaveInvoices(_filePath, _invoices);
        }

        /// <summary>
        /// Xóa một hóa đơn dựa trên ID.
        /// </summary>
        /// <param name="invoiceId">ID của hóa đơn cần xóa.</param>
        public void Delete(int invoiceId)
        {
            // Tìm hóa đơn cần xóa
            Node<Invoice> invoice = _invoices.Find(i => i.Id == invoiceId);
            _invoices.RemoveNode(invoice);

            // Lưu danh sách hóa đơn vào tệp
            DataManager.SaveInvoices(_filePath, _invoices);
        }

        /// <summary>
        /// Lấy thông tin của một hóa đơn dựa trên ID.
        /// </summary>
        /// <param name="id">ID của hóa đơn cần lấy thông tin.</param>
        /// <returns>Hóa đơn thỏa mãn ID hoặc null nếu không tìm thấy.</returns>
        public Invoice GetById(int id)
        {
            return _invoices.Find(i => i.Id == id)?.Data;
        }
    }
}
