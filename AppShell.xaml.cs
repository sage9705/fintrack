using FinTrack.Views;

namespace FinTrack;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("AddTransaction", typeof(AddTransactionPage));
        Routing.RegisterRoute("EditTransaction", typeof(EditTransactionPage));
    }
}