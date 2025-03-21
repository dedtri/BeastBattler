namespace TravelTime.Client.Mobile.Models
{
    public class Flight
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Price { get; set; }
        public string Airline { get; set; }
        public string FlightDuration { get; set; }
        public string Stops { get; set; }
        public string Layovers { get; set; }
    }
}