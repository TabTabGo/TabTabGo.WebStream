namespace TabTabGo.WebStream.Model
{
    public class UserIdData
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }

        public static UserIdData From(string userId, string tenantId)
        {
            return new UserIdData { TenantId = tenantId, UserId = userId };
        }
    }
}
