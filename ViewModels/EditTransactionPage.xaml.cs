using FinTrack.ViewModels;

namespace FinTrack.Views
{
    public partial class EditTransactionPage : ContentPage
    {
        private readonly EditTransactionViewModel _viewModel;

        public EditTransactionPage(EditTransactionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadTransactionAsync();
        }
    }
}