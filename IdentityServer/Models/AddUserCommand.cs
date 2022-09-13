namespace IdentityServer.Services;

public partial class KafkaProduser
{
    public class AddUserCommand
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}