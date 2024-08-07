using System.Collections.Generic;
using System.Linq;

namespace TabTabGo.WebStream.Model
{
    public class UserIdData
    {
        public string UserId { get; set; } 
        public string TenantId { get; set; } 
        public UserIdData()
        {

        }
        public UserIdData(string userId)
        {
            this.UserId = userId;
        } 
        public UserIdData(string userId, string tenantId) : this(userId)
        {
            this.TenantId = tenantId;
        }   
        public static UserIdData From(string userId, string tenantId)
        {
            return new UserIdData(userId,tenantId);
        } 
    }
}
