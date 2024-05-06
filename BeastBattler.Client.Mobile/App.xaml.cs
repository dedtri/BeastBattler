using BeastBattler.Client.Mobile.Services;

namespace BeastBattler.Client.Mobile
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
