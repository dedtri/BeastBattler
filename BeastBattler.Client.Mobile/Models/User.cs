using BeastBattler.Shared.DataModels;
using System.Collections.ObjectModel;

namespace BeastBattler.Client.Mobile.Models
{
    public class User : BaseClientModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserRoleTypeEnum Rank { get; set; }
        public int Loss { get; set; }
        public int Win { get; set; }

        #region Relations
        public virtual ICollection<Creature> Creatures { get; set; } = new Collection<Creature>();
        #endregion
    }
}
