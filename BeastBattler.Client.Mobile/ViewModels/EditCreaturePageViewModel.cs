using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace BeastBattler.Client.Mobile.ViewModels
{
    [QueryProperty("Creature", "Creature")]
    public partial class EditCreaturePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        Creature creature;

        public EditCreaturePageViewModel()
        {
            Title = "Claim a creature";
            Creature = new Creature();
        }

        #region AddCommand
        [RelayCommand]
        async Task Save()
        {
            var name = Creature.Name;
            var race = Creature.Race;
            var cageId = Creature.CageId;
            var userId = Creature.UserId;

            // Create JSON data
            var jsonData = $"{{\"name\":\"{name}\", \"race\":\"{race}\", \"cageId\":\"{cageId}\", \"userId\":\"{userId}\"}}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            var response = await httpClient.PutAsync("http://10.0.2.2:7083/api/Creature/" + Creature.Id, content);

            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.GoToAsync($"//{nameof(CreaturesPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Youre either missing a field or wrong the cage youre trying doesnt exist!", "OK");
            }
        }
        #endregion

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
