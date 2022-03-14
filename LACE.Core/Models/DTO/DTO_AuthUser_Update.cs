namespace LACE.Core.Models.DTO
{
    public class DTO_AuthUser_Update : DTO_AuthUser_Register
    {
        public string OldPassword { get; set; }
    }
}
