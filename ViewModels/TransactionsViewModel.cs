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
        private readonly CategoryService _categoryService;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        public TransactionsViewModel(ITransactionService transactionService, CategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
            Transactions = new ObservableCollection<Transaction>();
            Categories = new ObservableCollection<Category>();
        }

        [RelayCommand]
        public async Task LoadTransactionsAsync()
        {
            var loadedTransactions = await _transactionService.GetTransactionsAsync();
            Transactions.Clear();
            foreach (var transaction in loadedTransactions)
            {
                if (SelectedCategory == null || transaction.CategoryId == SelectedCategory.Id)
                {
                    Transactions.Add(transaction);
                }
            }
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

        partial void OnSelectedCategoryChanged(Category value)
        {
            LoadTransactionsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task NavigateToAddTransactionAsync()
        {
            await Shell.Current.GoToAsync("AddTransaction");
        }

        [RelayCommand]
        private async Task NavigateToEditTransactionAsync(Transaction transaction)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "TransactionId", transaction.Id }
            };
            await Shell.Current.GoToAsync("EditTransaction", navigationParameter);
        }

        [RelayCommand]
        private async Task DeleteTransactionAsync(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            await LoadTransactionsAsync();
        }
    }
}