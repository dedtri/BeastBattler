using Newtonsoft.Json.Linq;
using System.Text;
using TravelTime.Client.Mobile.Models;
using TravelTime.Client.Mobile.Helpers;

namespace TravelTime.Client.Mobile.Services
{
    public class AmadeusApiService
    {
        private readonly HttpClient _httpClient;

        public AmadeusApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var requestBody = new StringContent(
                "grant_type=client_credentials&client_id=x1ME41KB4zJTJxiBVmgNnyLIUJoNDoXg&client_secret=2RZifQsBDy6AF1S3",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await _httpClient.PostAsync("https://test.api.amadeus.com/v1/security/oauth2/token", requestBody);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(jsonResponse).SelectToken("access_token")?.ToString();

            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Access token is missing or invalid.");

            return token;
        }

        public async Task<List<Flight>> GetFlightOffersAsync(string from, string to, DateTime fromDate, DateTime toDate)
        {
            var token = await GetAccessTokenAsync();
            string formattedFromDate = fromDate.ToString("yyyy-MM-dd");
            string formattedToDate = toDate.ToString("yyyy-MM-dd");

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode={from}&destinationLocationCode={to}&departureDate={formattedFromDate}&returnDate={formattedToDate}&adults=1");

            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return new List<Flight>(); // Return empty if request fails

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var flightResults = new List<Flight>();

            var data = JObject.Parse(jsonResponse)["data"];
            foreach (var flight in data)
            {
                var flightDetails = new Flight
                {
                    FlightNumber = flight["id"]?.ToString(),
                    DepartureAirport = flight["itineraries"]?[0]?["segments"]?[0]?["departure"]?["iataCode"]?.ToString(),
                    ArrivalAirport = flight["itineraries"]?[0]?["segments"]?.LastOrDefault()?["arrival"]?["iataCode"]?.ToString(),
                    DepartureTime = flight["itineraries"]?[0]?["segments"]?[0]?["departure"]?["at"]?.ToString(),
                    ArrivalTime = flight["itineraries"]?[0]?["segments"]?[0]?["arrival"]?["at"]?.ToString(),
                    Price = flight["price"]?["total"]?.ToString() + " " + flight["price"]?["currency"]?.ToString(),
                    Airline = flight["validatingAirlineCodes"]?.FirstOrDefault()?.ToString(),
                    FlightDuration = FlightHelper.ConvertDurationToUserFriendlyFormat(flight["itineraries"]?[0]?["duration"]?.ToString()),
                    Stops = GetStopInfo(flight),
                    Layovers = GetLayoverAirports(flight)
                };

                flightResults.Add(flightDetails);
            }

            flightResults = flightResults.OrderBy(f => Decimal.Parse(f.Price.Split(' ')[0])).Take(10).ToList();

            return flightResults;
        }
        private string GetStopInfo(JToken flight)
        {
            var segments = flight["itineraries"]?[0]?["segments"];
            int stopCount = segments?.Count() - 1 ?? 0; 

            return stopCount switch
            {
                0 => "Non-stop",
                1 => "1 Stop",
                2 => "2 Stops",
                _ => $"{stopCount} Stops"
            };
        }

        private string GetLayoverAirports(JToken flight)
        {
            var segments = flight["itineraries"]?[0]?["segments"];
            var layovers = new List<string>();

            if (segments != null)
            {
                foreach (var segment in segments.Skip(1))
                {
                    var layoverAirport = segment["departure"]?["iataCode"]?.ToString();
                    if (!string.IsNullOrEmpty(layoverAirport))
                    {
                        layovers.Add(layoverAirport);
                    }
                }
            }

            return layovers.Any() ? string.Join(", ", layovers) : "No layovers";
        }
    }
}
