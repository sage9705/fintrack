using FinTrack.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddTransactionAsync(Transaction transaction)
        {
            transaction.Id = _transactions.Count + 1;
            _transactions.Add(transaction);
            await Task.CompletedTask;
        }
    }
}