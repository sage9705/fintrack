using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FinTrack.ViewModels
{
    [QueryProperty(nameof(TransactionId), "id")]
    public partial class EditTransactionViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        private int transactionId;

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private bool isIncome;

        public EditTransactionViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task LoadTransactionAsync()
        {
            var transaction = await _transactionService.GetTransactionAsync(TransactionId);
            Amount = transaction.Amount;
            Description = transaction.Description;
            Date = transaction.Date;
            IsIncome = transaction.IsIncome;
        }

        [RelayCommand]
        private async Task UpdateTransactionAsync()
        {
            var transaction = new Transaction
            {
                Id = TransactionId,
                Amount = Amount,
                Description = Description,
                Date = Date,
                IsIncome = IsIncome
            };

            await _transactionService.UpdateTransactionAsync(transaction);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DeleteTransactionAsync()
        {
            await _transactionService.DeleteTransactionAsync(TransactionId);
            await Shell.Current.GoToAsync("..");
        }
    }
}