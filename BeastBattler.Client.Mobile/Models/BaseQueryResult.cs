namespace BeastBattler.Client.Mobile.Models
{
    public class BaseQueryResult<T>
    {
        public IEnumerable<T> Entities { get; set; }
        public int Count { get; set; }
    }
}
