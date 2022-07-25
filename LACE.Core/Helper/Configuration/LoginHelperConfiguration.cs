using Nedesk.Core.Enums;

namespace LACE.Core.Helper.Configuration
{
    public class LoginHelperConfiguration
    {
        public string? Secret { get; set; }
        public NDHashAlgorithmEnum Algorithm { get; set; } = NDHashAlgorithmEnum.HMACSHA512;
    }
}
