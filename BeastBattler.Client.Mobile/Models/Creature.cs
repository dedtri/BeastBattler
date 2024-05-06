namespace BeastBattler.Client.Mobile.Models
{
    public class Creature : BaseClientModel
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Hitpoints { get; set; }
        public int Attack {  get; set; }
        public bool Status { get; set; }
        public string? ImageUrl { get; set; }

        #region Relations
        public virtual Cage Cage { get; set; }
        public int? CageId { get; set; }
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        #endregion
    }
}
