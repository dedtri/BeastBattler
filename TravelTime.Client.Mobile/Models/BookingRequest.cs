namespace TravelTime.Client.Mobile.Models
{
    public class BookingRequest
    {
        public int UserId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Notes { get; set; }
    }
}