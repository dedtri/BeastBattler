using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using TravelTime.Client.Mobile.Models;
using TravelTime.Client.Mobile.Services;

namespace TravelTime.Client.Mobile.ViewModels
{
    public partial class FlightSearchViewModel : BaseViewModel
    {
        [ObservableProperty]
        public User user;

        private readonly SessionService _sessionService;
        private readonly AmadeusApiService _amadeusApiService;

        public ObservableCollection<Flight> FlightResults { get; set; } = new();
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSearchViewModel(
            SessionService sessionService,
            AmadeusApiService amadeusApiService
            )
        {
            Title = "Search For Flights";
            _sessionService = sessionService;
            _amadeusApiService = amadeusApiService;
        }

        public async Task SearchFlights(string from, string to, DateTime fromDate, DateTime toDate)
        {
            var flights = await _amadeusApiService.GetFlightOffersAsync(from.ToUpper(), to.ToUpper(), fromDate, toDate);

            FlightResults.Clear();
            foreach (var flight in flights)
            {
                FlightResults.Add(flight);
            }

            OnPropertyChanged(nameof(FlightResults));
        }

        [RelayCommand]
        async Task BookFlightAsync(Flight flight)
        {
            var request = new BookingRequest
            {
                UserId = _sessionService.UserId,
                Origin = flight.DepartureAirport,
                Destination = flight.ArrivalAirport,
                StartDate = flight.DepartureTime,
                EndDate = flight.ArrivalTime,
                Notes = $"Total Duration of Flight: {flight.FlightDuration}\n Stops: {flight.Stops}\n Airline: {flight.Airline}\n Layovers: {flight.Layovers}"
            };

            var httpClient = new HttpClient();
            HttpResponseMessage response = null;

            // Correct API endpoint URL for the Create action
            string url = "";
            if (DeviceInfo.Platform == DevicePlatform.Android)
                url = "http://10.0.2.2:5247/api/Trip";
            else
                url = "http://localhost:5247/api/Trip";

            try
            {
                response = await httpClient.PostAsJsonAsync(url, request);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Booking Successful", $"Your trip to {flight.ArrivalAirport} is confirmed!", "OK");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Booking Failed", $"Error: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Booking Failed", $"There was an issue with your booking. Please try again.\nError: {ex.Message}", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
