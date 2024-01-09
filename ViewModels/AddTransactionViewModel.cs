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
    public partial class AddTransactionViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;
        private readonly CategoryService _categoryService;

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private bool isIncome;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        public AddTransactionViewModel(ITransactionService transactionService, CategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            Categories = new ObservableCollection<Category>();
        }

        [RelayCommand]
        public async Task LoadCategoriesAsync()
        {
            var loadedCategories = await _categoryService.GetCategoriesAsync();
            Categories.Clear();
            foreach (var category in loadedCategories)
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        private async Task AddTransactionAsync()
        {
            if (SelectedCategory == null)
            {
                // Show an error message or handle the case where no category is selected
                return;
            }

            var newTransaction = new Transaction
            {
                Amount = Amount,
                Description = Description,
                Date = Date,
                IsIncome = IsIncome,
                CategoryId = SelectedCategory.Id
            };

            await _transactionService.AddTransactionAsync(newTransaction);

            // Reset the form
            Amount = 0;
            Description = string.Empty;
            Date = DateTime.Now;
            IsIncome = false;
            SelectedCategory = null;

            // Navigate back to the transactions list
            await Shell.Current.GoToAsync("..");
        }
    }
}