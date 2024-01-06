using FinTrack.ViewModels;

namespace FinTrack.Views
{
    public partial class AddTransactionPage : ContentPage
    {
        public AddTransactionPage(AddTransactionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}