using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class EditCreaturePage : ContentPage
{
    public EditCreaturePage(EditCreaturePageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}