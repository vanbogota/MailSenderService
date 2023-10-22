namespace FinalProject.Services
{
    public class MailGatewayOptions
    {
        public int Port { get; set; }
        public string Password { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Server { get; set; } = null!;
    }
}