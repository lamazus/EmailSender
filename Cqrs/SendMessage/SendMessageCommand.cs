using MediatR;

namespace EmailSender.Cqrs.SendMessage
{
    public class SendMessageCommand : IRequest
    {
        /// <summary>
        /// Тема сообщения
        /// </summary>
        public string Subject { get; set; } = string.Empty;
        /// <summary>
        /// Содержимое
        /// </summary>
        public string Body { get; set; } = string.Empty;
        /// <summary>
        /// Массив адресов электронной почты пользователей
        /// </summary>
        public List<string> Recipients { get; set; } = new();
    }
}