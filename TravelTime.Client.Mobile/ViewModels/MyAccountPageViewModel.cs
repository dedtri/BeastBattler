using TravelTime.Client.Mobile.Models;
using TravelTime.Client.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace TravelTime.Client.Mobile.ViewModels
{
    public partial class MyAccountPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public User user;

        [ObservableProperty]
        public bool canShowRoute;

        private readonly SessionService sessionService;
        private Dictionary<string, Airport> _airports;
        private Dictionary<string, Airport> _iataAirports = new Dictionary<string, Airport>();

        public MyAccountPageViewModel(SessionService sessionService)
        {
            Title = "My Trips";
            this.sessionService = sessionService;
        }

        [RelayCommand]
        async Task GetPlannedTrips()
        {
            CanShowRoute = false;
            try
            {
                var userId = this.sessionService.UserId;
                var httpClient = new HttpClient();

                var response = new HttpResponseMessage();
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    response = await httpClient.GetAsync("http://10.0.2.2:5247/api/User/id/" + userId);
                else
                    response = await httpClient.GetAsync("http://localhost:5247/api/User/id/" + userId);

                if (response.IsSuccessStatusCode)
                {
                    User = await response.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR", $"Failed to retrieve trips: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception while getting trips: {ex}");
                // Handle exception
            }
        }

        public async Task LoadAirportData()
        {
            try
            {
                using Stream stream = await FileSystem.OpenAppPackageFileAsync("airports.json");
                using StreamReader reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                _airports = JsonSerializer.Deserialize<Dictionary<string, Airport>>(json, options);

                foreach (var airport in _airports.Values)
                {
                    if (!string.IsNullOrEmpty(airport.Iata))
                    {
                        _iataAirports.TryAdd(airport.Iata, airport);
                    }
                }
                CanShowRoute = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading airport data: {ex.Message}");
                _airports = new Dictionary<string, Airport>();
            }
        }

        private string GetAirportName(string iataCode) //Using IATA code now.
        {
            if (_iataAirports != null && _iataAirports.TryGetValue(iataCode, out Airport airport))
            {
                return airport.Name;
            }
            return iataCode; // Return the IATA code if not found.
        }

        [RelayCommand]
        public async Task ShowRouteAsync(Trip trip)
        {
            if (trip == null || string.IsNullOrEmpty(trip.Origin)) // Only need the destination IATA now.
            {
                return;
            }

            string fromAddress = await Application.Current.MainPage.DisplayPromptAsync("Enter Your Address", "Please enter your starting address:");

            if (string.IsNullOrEmpty(fromAddress))
            {
                return; // User cancelled or entered nothing.
            }

            string toAirportName = GetAirportName(trip.Origin); // trip.Origin is the IATA code.

            string encodedFromAddress = Uri.EscapeDataString(fromAddress);
            string encodedToAddress = Uri.EscapeDataString(toAirportName);

            string mapUri = $"http://maps.google.com/maps?saddr={encodedFromAddress}&daddr={encodedToAddress}";

            try
            {
                await Launcher.Default.OpenAsync(mapUri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening map: {ex.Message}");
            }
        }

        [RelayCommand]
        async Task Delete()
        {
            // TODO
        }
    }
}
