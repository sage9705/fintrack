using FinTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTrack.Services
{
    public class SQLiteTransactionService : ITransactionService
    {
        private readonly DatabaseContext _context;

        public SQLiteTransactionService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _context.GetTransactionsAsync();
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            await _context.SaveTransactionAsync(transaction);
            return transaction;
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            await _context.SaveTransactionAsync(transaction);
            return transaction;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.GetTransactionAsync(id);
            if (transaction != null)
            {
                await _context.DeleteTransactionAsync(transaction);
            }
        }
    }
}