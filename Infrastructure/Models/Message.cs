namespace EmailSender.Infrastructure.Models
{
    public class Message
    {
        public Guid MessageId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Recipients { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string FailedMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}