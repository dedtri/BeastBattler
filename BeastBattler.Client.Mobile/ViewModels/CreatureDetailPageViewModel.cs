using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeastBattler.Client.Mobile.ViewModels
{
    [QueryProperty("Creature", "Creature")]
    public partial class CreatureDetailPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Creature creature;
        public CreatureDetailPageViewModel()
        {
            Title = "Detail of Creature";
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        async Task UpdateAsync()
        {
            await Shell.Current.GoToAsync($"EditCreaturePage?id={Creature.Name}", true,
            new Dictionary<string, object>
            {
                    {"Creature", Creature}
            });
        }

        [RelayCommand]
        async Task DeleteAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync("http://10.0.2.2:7083/api/Creature/delete/" + Creature.Id);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("SUCCESS", $"Creature has succesfully been released back to the wild.", "OK");
                await Shell.Current.GoToAsync($"//{nameof(CreaturesPage)}", true);
            }   
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Failed to release the creature!!", "OK");
            }
        }

    }
}
