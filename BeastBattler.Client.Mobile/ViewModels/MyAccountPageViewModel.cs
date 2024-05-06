using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class MyAccountPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public User user;

        private readonly SessionService sessionService;

        public MyAccountPageViewModel(SessionService sessionService)
        {
            Title = "My Account";
            this.sessionService = sessionService;
        }

        [RelayCommand]
        async Task GetCreaturesAsync()
        {
            try
            {
                var userId = this.sessionService.UserId;
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://10.0.2.2:7083/api/User/id/" + userId);

                if (response.IsSuccessStatusCode)
                {
                    User = await response.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR", $"Failed to retrieve cages: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception while getting cages: {ex}");
                // Handle exception
            }
        }

        [RelayCommand]
        async Task Detele(Cage cage)
        {
            // TODO
        }
    }
}
