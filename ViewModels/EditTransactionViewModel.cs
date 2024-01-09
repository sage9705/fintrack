using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FinTrack.ViewModels
{
    [QueryProperty(nameof(TransactionId), "id")]
    public partial class EditTransactionViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;
        private readonly CategoryService _categoryService;

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

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        public EditTransactionViewModel(ITransactionService transactionService, CategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            Categories = new ObservableCollection<Category>();
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            await LoadCategoriesAsync();
            await LoadTransactionAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            var loadedCategories = await _categoryService.GetCategoriesAsync();
            Categories.Clear();
            foreach (var category in loadedCategories)
            {
                Categories.Add(category);
            }
        }

        private async Task LoadTransactionAsync()
        {
            var transaction = await _transactionService.GetTransactionAsync(TransactionId);
            if (transaction != null)
            {
                Amount = transaction.Amount;
                Description = transaction.Description;
                Date = transaction.Date;
                IsIncome = transaction.IsIncome;
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == transaction.CategoryId);
            }
        }

        [RelayCommand]
        private async Task UpdateTransactionAsync()
        {
            if (SelectedCategory == null)
            {
                // Show an error message or handle the case where no category is selected
                return;
            }

            var updatedTransaction = new Transaction
            {
                Id = TransactionId,
                Amount = Amount,
                Description = Description,
                Date = Date,
                IsIncome = IsIncome,
                CategoryId = SelectedCategory.Id
            };

            await _transactionService.UpdateTransactionAsync(updatedTransaction);

            // Navigate back to the transactions list
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