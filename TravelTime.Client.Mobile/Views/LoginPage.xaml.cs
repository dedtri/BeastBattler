using TravelTime.Client.Mobile.ViewModels;

namespace TravelTime.Client.Mobile.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginPageViewModel _viewModel;

    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        _viewModel.User = new Models.User();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}