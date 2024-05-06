using BeastBattler.Client.Mobile.Services;
using BeastBattler.Client.Mobile.Views;

namespace BeastBattler.Client.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly SessionService sessionService;

        public AppShell(SessionService sessionService)
        {
            InitializeComponent();

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));

            Routing.RegisterRoute("CagesPage", typeof(CagesPage));
            Routing.RegisterRoute("AddCagePage", typeof(AddCagePage));
            Routing.RegisterRoute("CageDetailPage", typeof(CageDetailPage));

            Routing.RegisterRoute("CreaturesPage", typeof(CreaturesPage));
            Routing.RegisterRoute("AddCreaturePage", typeof(AddCreaturePage));
            Routing.RegisterRoute("CreatureDetailPage", typeof(CreatureDetailPage));
            Routing.RegisterRoute("EditCreaturePage", typeof(EditCreaturePage));

            Routing.RegisterRoute("MyAccountPage", typeof(MyAccountPage));
            this.sessionService = sessionService;
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            this.sessionService.ClearUserId();
            await Current.GoToAsync($"/{nameof(LoginPage)}", true);
        }
    }
}
