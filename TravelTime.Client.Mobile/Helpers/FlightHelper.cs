namespace TravelTime.Client.Mobile.Helpers
{
    public static class FlightHelper
    {
        public static string ConvertDurationToUserFriendlyFormat(string flightDuration)
        {
            if (string.IsNullOrEmpty(flightDuration)) return "N/A";

            var duration = flightDuration.Substring(2);
            string outputString = duration.Replace("H", " Hours ").Replace("M", " Minutes");

            return outputString;
        }
    }
}
