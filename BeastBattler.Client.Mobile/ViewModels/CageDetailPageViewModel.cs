using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeastBattler.Client.Mobile.ViewModels
{
    [QueryProperty("Cage", "Cage")]
    public partial class CageDetailPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Cage cage;

        public CageDetailPageViewModel()
        {
            Title = "Cage Detail";
        }

        [RelayCommand]
        async Task DeleteAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync("http://10.0.2.2:7083/api/Cage/delete/" + Cage.Id);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("SUCCESS", $"Cage has succesfully been demolished.", "OK");
                await Shell.Current.GoToAsync($"///{nameof(CagesPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Failed to release the creature!!", "OK");
            }
        }
    }
}
