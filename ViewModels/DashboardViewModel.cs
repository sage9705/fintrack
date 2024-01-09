using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinTrack.Models;
using FinTrack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinTrack.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        private decimal totalIncome;

        [ObservableProperty]
        private decimal totalExpenses;

        [ObservableProperty]
        private decimal balance;

        [ObservableProperty]
        private List<Transaction> recentTransactions;

        public DashboardViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [RelayCommand]
        public async Task LoadDashboardDataAsync()
        {
            var allTransactions = await _transactionService.GetTransactionsAsync();

            TotalIncome = allTransactions.Where(t => t.IsIncome).Sum(t => t.Amount);
            TotalExpenses = allTransactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
            Balance = TotalIncome - TotalExpenses;

            RecentTransactions = allTransactions
                .OrderByDescending(t => t.Date)
                .Take(5)
                .ToList();
        }
    }
}