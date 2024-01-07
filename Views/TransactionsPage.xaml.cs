using FinTrack.ViewModels;

namespace FinTrack.Views
{
    public partial class TransactionsPage : ContentPage
    {
        private readonly TransactionsViewModel _viewModel;

        public TransactionsPage(TransactionsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadTransactionsCommand.ExecuteAsync(null);
        }

        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "TransactionsViewModel", _viewModel }
            };
            await Shell.Current.GoToAsync("AddTransaction", navigationParameter);
        }
    }
}