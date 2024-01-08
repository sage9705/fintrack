using FinTrack.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace FinTrack.Services
{
    public class MockTransactionService : ITransactionService
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public MockTransactionService()
        {
            // Add some mock data
            _transactions.Add(new Transaction { Id = 1, Amount = 1000, Description = "Salary", Date = DateTime.Now.AddDays(-5), IsIncome = true });
            _transactions.Add(new Transaction { Id = 2, Amount = 50, Description = "Groceries", Date = DateTime.Now.AddDays(-2), IsIncome = false });
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await Task.FromResult(_transactions);
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await Task.FromResult(_transactions.FirstOrDefault(t => t.Id == id));
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            transaction.Id = _transactions.Count + 1;
            _transactions.Add(transaction);
            return await Task.FromResult(transaction);
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = _transactions.FirstOrDefault(t => t.Id == transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Description = transaction.Description;
                existingTransaction.Date = transaction.Date;
                existingTransaction.IsIncome = transaction.IsIncome;
            }
            return await Task.FromResult(existingTransaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                _transactions.Remove(transaction);
            }
            await Task.CompletedTask;
        }
    }
}