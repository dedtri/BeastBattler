using TravelTime.Shared.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelTime.Backend.Database.DataModels
{
    [Table("users")]
    public class User : BaseDataModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoleTypeEnum Rank {  get; set; }

        #region Relations
        public virtual ICollection<Trip> Trips { get; set; } = new Collection<Trip>();
        #endregion
    }
}
