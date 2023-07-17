namespace EmailSender.Cqrs.GetMessages
{
    public class MessagesResponse
    {
        public Guid MessageId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<string> Recipients { get; set; } = new();
        public string Result { get; set; } = string.Empty;
        public string FailedMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}