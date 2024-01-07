using SQLite;
using FinTrack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinTrack.Services
{
    public class DatabaseContext
    {
        private SQLiteAsyncConnection _database;

        public DatabaseContext()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fintrack.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _database.Table<Transaction>().ToListAsync();
        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            return await _database.Table<Transaction>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Id != 0)
            {
                return await _database.UpdateAsync(transaction);
            }
            else
            {
                return await _database.InsertAsync(transaction);
            }
        }

        public async Task<int> DeleteTransactionAsync(Transaction transaction)
        {
            return await _database.DeleteAsync(transaction);
        }
    }
}