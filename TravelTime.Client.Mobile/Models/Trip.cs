using System.ComponentModel.DataAnnotations.Schema;

namespace TravelTime.Client.Mobile.Models
{
    [Table("trips")]
    public class Trip : BaseClientModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        #region Relations
        public int UserId { get; set; }
        #endregion
    }
}
