using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class AddCagePage : ContentPage
{
    public AddCagePage(AddCagePageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}