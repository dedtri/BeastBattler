using BeastBattler.Shared.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeastBattler.Backend.Database.DataModels
{
    [Table("users")]
    public class User : BaseDataModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoleTypeEnum Rank {  get; set; }
        public int Loss { get; set; }
        public int Win { get; set; }

        #region Relations
        public virtual ICollection<Creature> Creatures { get; set; } = new Collection<Creature>();
        #endregion
    }
}
