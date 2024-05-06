using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Services;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class CagesPageViewModel : BaseViewModel
    {
        public ObservableCollection<Cage> Cages { get; } = new ObservableCollection<Cage>();

        private readonly IConnectivity connectivity;

        public CagesPageViewModel(IConnectivity connectivity)
        {
            Title = "Overview of Cages";

            this.connectivity = connectivity;
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Cage cage)
        {
            if (cage is null)
                return;

            await Shell.Current.GoToAsync($"CageDetailPage?id={cage.Name}", true,
                new Dictionary<string, object>
                {
                    {"Cage", cage}
                });
        }

        [RelayCommand]
        async Task GoAddCageAsync()
        {
            await Shell.Current.GoToAsync($"/{nameof(AddCagePage)}", true);
        }

        [RelayCommand]
        async Task GetCagesAsync()
        {
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Error!", $"Failed to connect to the internet.", "OK");
                    return;
                }

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://10.0.2.2:7083/api/Cage");

                if (response.IsSuccessStatusCode)
                {
                    var baseQueryResult = await response.Content.ReadFromJsonAsync<BaseQueryResult<Cage>>();
                    if (baseQueryResult != null)
                    {
                        Cages.Clear();
                        foreach (var cage in baseQueryResult.Entities)
                        {
                            Cages.Add(cage);
                        }
                    }
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

    }
}
