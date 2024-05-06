using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class CagesPage : ContentPage
{
    private readonly CagesPageViewModel viewModel;

    public CagesPage(CagesPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        this.viewModel.GetCagesCommand.Execute(this);
    }
}