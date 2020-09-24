namespace MvcProjectDbConn
{
    public class UserClient
    {
        public int id { get; set; }
        public string TtpUserId { get; set; }
        public virtual TtpUser TtpUser { get; set; }
        public int ClientId { get; set;}
        public virtual Client Client { get; set; }
        public bool IsDeleted { get; set; }
    }
}