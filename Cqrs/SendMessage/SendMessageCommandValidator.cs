using FluentValidation;

namespace EmailSender.Cqrs.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Необходимо указать тему сообщения не может быть пустым");
            
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Необходимо указать содержимое сообщения не может быть пустым");

            RuleFor(x => x.Recipients).NotEmpty().WithMessage("Список email-адресов не может быть пустым");
            
            RuleForEach(x => x.Recipients) 
                .NotEmpty().WithMessage("Необходимо указать электронную почту пользователей")
                .EmailAddress().WithMessage("Email не соответствует формату \"email@example.ru\"");
        }
    }
}