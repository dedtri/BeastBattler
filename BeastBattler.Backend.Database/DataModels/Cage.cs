using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeastBattler.Backend.Database.DataModels
{
    [Table("cages")]
    public class Cage : BaseDataModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Capacity { get; set; }

        #region Relations
        public virtual ICollection<Creature> Creatures { get; set; } = new Collection<Creature>();
        #endregion
    }
}
