using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FinTrack.ViewModels
{
    public partial class TransactionsViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        public TransactionsViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
            Transactions = new ObservableCollection<Transaction>();
        }

        [RelayCommand]
        private async Task LoadTransactionsAsync()
        {
            var loadedTransactions = await _transactionService.GetTransactionsAsync();
            Transactions.Clear();
            foreach (var transaction in loadedTransactions)
            {
                Transactions.Add(transaction);
            }
        }
    }
}