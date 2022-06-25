namespace FinalProject.Services
{
    public class MailGatewayOptions
    {
        private const int DefaultPort = 25;
        public MailGatewayOptions()
        {
            Port = DefaultPort;
        }
        public int Port { get; set; }
        public string Password { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string SMTPServer { get; set; } = null!;
    }
}