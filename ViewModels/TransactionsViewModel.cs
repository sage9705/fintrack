using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

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
        public async Task LoadTransactionsAsync()
        {
            var loadedTransactions = await _transactionService.GetTransactionsAsync();
            Transactions.Clear();
            foreach (var transaction in loadedTransactions)
            {
                Transactions.Add(transaction);
            }
        }

        [RelayCommand]
        private async Task NavigateToAddTransactionAsync()
        {
            await Shell.Current.GoToAsync("AddTransaction");
        }

        [RelayCommand]
        private async Task NavigateToEditTransactionAsync(Transaction transaction)
        {
            await Shell.Current.GoToAsync($"EditTransaction?id={transaction.Id}");
        }

        public async Task RefreshTransactions()
        {
            await LoadTransactionsAsync();
        }
    }
}