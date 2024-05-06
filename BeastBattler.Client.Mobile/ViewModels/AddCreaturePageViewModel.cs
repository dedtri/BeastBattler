using BeastBattler.Client.Mobile.Models;
using BeastBattler.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace BeastBattler.Client.Mobile.ViewModels
{
    public partial class AddCreaturePageViewModel : BaseViewModel
    {

        [ObservableProperty]
        Creature creature;

        public AddCreaturePageViewModel()
        {
            Title = "Add a creature";
            Creature = new Creature(); 
        }

        #region AddCommand
        [RelayCommand]
        async Task Add()
        {
            var name = Creature.Name; 
            var race = Creature.Race;
            var cageId = Creature.CageId;

            // Create JSON data
            var jsonData = $"{{\"name\":\"{name}\", \"race\":\"{race}\", \"cageId\":\"{cageId}\"}}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync("http://10.0.2.2:7083/api/Creature", content);

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
    }
}
