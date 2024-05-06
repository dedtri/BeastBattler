namespace BeastBattler.Client.Mobile.Services
{
    public class SessionService
    {
        private int userId;

        public int UserId => userId;

        public void SetUserId(int newUserId)
        {
            userId = newUserId;
        }

        public void ClearUserId()
        {
            userId = 0; // or any other value that signifies no user logged in
        }
    }
}