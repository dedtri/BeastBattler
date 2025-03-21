using TravelTime.Client.Mobile.Services;
using TravelTime.Client.Mobile.Views;

namespace TravelTime.Client.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly SessionService sessionService;

        public AppShell(SessionService sessionService)
        {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("MyAccountPage", typeof(MyAccountPage));
            Routing.RegisterRoute("FlightSearchPage", typeof(FlightSearchPage));

            this.sessionService = sessionService;
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            this.sessionService.ClearUserId();
            await Current.GoToAsync($"/{nameof(LoginPage)}", true);
        }
    }
}
