using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class CreaturesPage : ContentPage
{
    private readonly CreaturesPageViewModel viewModel;

    public CreaturesPage(CreaturesPageViewModel viewModel)
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