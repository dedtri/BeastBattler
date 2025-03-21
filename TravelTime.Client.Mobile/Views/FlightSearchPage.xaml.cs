using TravelTime.Client.Mobile.ViewModels;

namespace TravelTime.Client.Mobile.Views;

public partial class FlightSearchPage : ContentPage
{
    private readonly FlightSearchViewModel _viewModel;

    public FlightSearchPage(FlightSearchViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    private async void OnSearchFlightsClicked(object sender, EventArgs e)
    {
        DateTime fromDate = FromDatePicker.Date;
        DateTime toDate = ToDatePicker.Date;

        // Pass the date range to the ViewModel's method
        await _viewModel.SearchFlights(
            FromEntry.Text,
            ToEntry.Text,
            fromDate,
            toDate
        );
    }
}