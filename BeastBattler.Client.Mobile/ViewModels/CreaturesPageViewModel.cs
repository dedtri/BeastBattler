using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class CreaturesPageViewModel : BaseViewModel
    {
        public ObservableCollection<Creature> Creatures { get; } = new ObservableCollection<Creature>();

        private readonly IConnectivity connectivity;

        public CreaturesPageViewModel(IConnectivity connectivity)
        {
            Title = "Overview of Creatures";

            this.connectivity = connectivity;
        }


        [RelayCommand]
        async Task GoToDetailsAsync(Creature creature)
        {
            if (creature is null)
                return;

            await Shell.Current.GoToAsync($"CreatureDetailPage?id={creature.Name}", true,
                new Dictionary<string, object>
                {
                    {"Creature", creature}
                });
        }

        [RelayCommand]
        async Task GoAddCreatureAsync()
        {
            await Shell.Current.GoToAsync($"/{nameof(AddCreaturePage)}", true);
        }

        [RelayCommand]
        async Task GetCreaturesAsync()
        {
            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Error!", $"Failed to connect to the internet.", "OK");
                    return;
                }

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync("http://10.0.2.2:7083/api/Creature");

                if (response.IsSuccessStatusCode)
                {
                    var baseQueryResult = await response.Content.ReadFromJsonAsync<BaseQueryResult<Creature>>();
                    if (baseQueryResult != null)
                    {
                        Creatures.Clear();
                        foreach (var cage in baseQueryResult.Entities)
                        {
                            Creatures.Add(cage);
                        }
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR", $"Failed to retrieve creatures: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception while getting creatures: {ex}");
                // Handle exception
            }
        }

    }
}
