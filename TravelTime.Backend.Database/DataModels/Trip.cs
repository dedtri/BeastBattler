using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelTime.Backend.Database.DataModels
{
    [Table("trips")]
    public class Trip : BaseDataModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }

        #region Relations
        public int UserId { get; set; }
        #endregion
    }
}
