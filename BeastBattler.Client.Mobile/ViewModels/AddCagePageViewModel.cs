using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class AddCagePageViewModel : BaseViewModel
    {

        [ObservableProperty]
        Cage cage;

        public AddCagePageViewModel()
        {
            Title = "Add a cage";
            Cage = new Cage(); 
        }

        #region AddCommand
        [RelayCommand]
        async Task Add()
        {
            var name = Cage.Name; 
            var capacity = Cage.Capacity; 

            // Create JSON data
            var jsonData = $"{{\"name\":\"{name}\", \"capacity\":\"{capacity}\"}}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync("http://10.0.2.2:7083/api/Cage", content);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.GoToAsync($"//{nameof(CagesPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Youre missing something!", "OK");
            }
        }
        #endregion
    }
}
