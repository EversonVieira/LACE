using Nedesk.Core.Interfaces;

namespace LACE.Core.Models
{
    public class AuthSession : ISession
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string SessionKey { get; set; }
        public DateTime LastRenewDate { get; set; }
    }
}
