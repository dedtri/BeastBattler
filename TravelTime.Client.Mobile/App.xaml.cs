using TravelTime.Client.Mobile.Services;

namespace TravelTime.Client.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SessionService Sessionservice = new SessionService();

            MainPage = new AppShell(Sessionservice);
        }
    }
}
