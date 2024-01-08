using FinTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTrack.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync();
        Task<Transaction> GetTransactionAsync(int id);
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Transaction> UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}