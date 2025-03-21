using TravelTime.Client.Mobile.Models;
using TravelTime.Client.Mobile.Services;
using TravelTime.Client.Mobile.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Runtime.InteropServices;

namespace TravelTime.Client.Mobile.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly SessionService sessionService;
        [ObservableProperty]
        User user;

        public LoginPageViewModel(SessionService sessionService)
        {
            Title = string.Empty;
            User = new User();
            this.sessionService = sessionService;
        }

        #region LoginCommand
        [RelayCommand]
        async Task Login()
        {
            var email = User.Email;
            var password = User.Password;

            // Create JSON data
            var jsonData = $"{{\"email\":\"{email}\", \"password\":\"{password}\"}}";
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            var response = new HttpResponseMessage();
            if (DeviceInfo.Platform == DevicePlatform.Android)
                response = await httpClient.PostAsync("http://10.0.2.2:5247/api/User/login", content);
            else
                response = await httpClient.PostAsync("http://localhost:5247/api/User/login", content);

            if (response.IsSuccessStatusCode)
            {
                // Login successful, handle response
                var responseContent = await response.Content.ReadAsStringAsync();
                var userId = int.Parse(responseContent); // Assuming userId is returned as plain text

                // Store userId in session service
                this.sessionService.SetUserId(userId);

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", $"Your username or password has been entered correctly!", "OK");
            }
        }
        #endregion

        [RelayCommand]
        async Task GoSignUpButton()
        {
            await Shell.Current.GoToAsync($"/{nameof(RegisterPage)}", true);
        }
    }
}
