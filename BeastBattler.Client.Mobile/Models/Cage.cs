using System.Collections.ObjectModel;

namespace BeastBattler.Client.Mobile.Models
{
    public class Cage : BaseClientModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public int Capacity { get; set; }

        #region Relations
        public virtual ICollection<Creature> Creatures { get; set; } = new Collection<Creature>();
        #endregion
    }
}
