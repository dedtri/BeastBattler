using TravelTime.Client.Mobile.ViewModels;

namespace TravelTime.Client.Mobile.Views;

public partial class MyAccountPage : ContentPage
{
    private readonly MyAccountPageViewModel viewModel;

    public MyAccountPage(MyAccountPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        this.viewModel.GetPlannedTripsCommand.Execute(this);
        await this.viewModel.LoadAirportData();
    }
}