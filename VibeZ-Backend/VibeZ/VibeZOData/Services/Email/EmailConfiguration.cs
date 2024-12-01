namespace VibeZOData.Services.Email
{
    public class EmailConfiguration
    {
        public string From { get; set; } = "kienlhtde180570@fpt.edu.vn";
        public string FromName { get; set; } = "Admin";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string Username { get; set; } = "kienlhtde180570@fpt.edu.vn";
        public string Password { get; set; } = "faym zqey hefm oeqc";
    }

}