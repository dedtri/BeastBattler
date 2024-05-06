using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class MyAccountPage : ContentPage
{
    private readonly MyAccountPageViewModel viewModel;

    public MyAccountPage(MyAccountPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        this.viewModel.GetCreaturesCommand.Execute(this);
    }
}