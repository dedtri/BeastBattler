using TravelTime.Client.Mobile.Models;
using TravelTime.Client.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;

namespace TravelTime.Client.Mobile.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        private readonly SessionService sessionService;
        [ObservableProperty]
        User user;

        public RegisterPageViewModel(SessionService sessionService)
        {
            Title = string.Empty;
            User = new User();
            this.sessionService = sessionService;
        }

        #region SignUpCommand
        [RelayCommand]
        async Task SignUp()
        {
            var email = User.Email; 
            var password = User.Password; 

            // Create JSON data
            var jsonData = $"{{\"email\":\"{email}\", \"password\":\"{password}\"}}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            var response = new HttpResponseMessage();
            if (DeviceInfo.Platform == DevicePlatform.Android)
                response = await httpClient.PostAsync("http://10.0.2.2:5247/api/User/register", content);
            else
                response = await httpClient.PostAsync("http://localhost:5247/api/User/register", content);

            if (response.IsSuccessStatusCode)
            {
                // Login successful, handle response
                var responseContent = await response.Content.ReadAsStringAsync();
                var userId = int.Parse(responseContent); // Assuming userId is returned as plain text

                // Store userId in session service
                this.sessionService.SetUserId(userId);

                await Shell.Current.DisplayAlert("SUCCESS", $"You've been registered in the database! Please proceed to the Main Menu", "OK");

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Another user with that email exists!!!", "OK");
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
