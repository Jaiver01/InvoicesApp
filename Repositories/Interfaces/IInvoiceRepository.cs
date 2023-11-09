using InvoicesApp.Models;

namespace InvoicesApp.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<InvoiceViewModel>> GetAllAsync();
        Task<InvoiceViewModel> GetByIdAsync(int id);
        Task<List<InvoiceViewModel>> GetAllByCustomerAsync(int customerId);
        Task<List<InvoiceViewModel>> GetAllByNumberAsync(int invoiceNumber);
        Task<InvoiceModel> CreateAsync(InvoiceModel invoice);
        Task<InvoiceModel> UpdateAsync(InvoiceModel invoice);
        Task<InvoiceModel> DeleteAsync(int id);
    }
}
