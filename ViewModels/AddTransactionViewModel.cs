using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System;
using System.Threading.Tasks;

namespace FinTrack.ViewModels
{
    public partial class AddTransactionViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private bool isIncome;

        public AddTransactionViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [RelayCommand]
        private async Task AddTransactionAsync()
        {
            var newTransaction = new Transaction
            {
                Amount = Amount,
                Description = Description,
                Date = Date,
                IsIncome = IsIncome
            };

            await _transactionService.AddTransactionAsync(newTransaction);

            // Reset the form
            Amount = 0;
            Description = string.Empty;
            Date = DateTime.Now;
            IsIncome = false;

            // You might want to navigate back to the transactions list or show a success message here
        }
    }
}