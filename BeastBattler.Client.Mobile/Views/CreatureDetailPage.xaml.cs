using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class CreatureDetailPage : ContentPage
{

    public CreatureDetailPage(CreatureDetailPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}