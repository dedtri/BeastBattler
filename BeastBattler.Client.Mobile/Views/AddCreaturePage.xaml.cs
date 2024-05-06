using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class AddCreaturePage : ContentPage
{
    public AddCreaturePage(AddCreaturePageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}