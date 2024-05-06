using BeastBattler.Client.Mobile.ViewModels;

namespace BeastBattler.Client.Mobile.Views;

public partial class CageDetailPage : ContentPage
{
    public CageDetailPage(CageDetailPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}