using FinTrack.ViewModels;

namespace FinTrack.Views
{
    public partial class TransactionsPage : ContentPage
    {
        public TransactionsPage(TransactionsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ((TransactionsViewModel)BindingContext).LoadTransactionsCommand.ExecuteAsync(null);
        }
    }
}