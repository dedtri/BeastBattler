using TravelTime.Shared.DataModels;
using System.Collections.ObjectModel;

namespace TravelTime.Client.Mobile.Models
{
    public class User : BaseClientModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoleTypeEnum Rank { get; set; }

        #region Relations
        public virtual ICollection<Trip> Trips { get; set; } = new Collection<Trip>();
        #endregion
    }
}
