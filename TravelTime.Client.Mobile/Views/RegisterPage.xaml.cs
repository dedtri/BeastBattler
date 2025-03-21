using TravelTime.Client.Mobile.ViewModels;

namespace TravelTime.Client.Mobile.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}