using TravelTime.Client.Mobile.Views;

namespace TravelTime.Client.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartPlanningClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FlightSearchPage));
        }
    }

}
