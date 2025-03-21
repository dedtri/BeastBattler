namespace TravelTime.Client.Mobile.Models
{
    public class Airport
    {
        public string Icao { get; set; }
        public string Iata { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Elevation { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Tz { get; set; }
    }
}