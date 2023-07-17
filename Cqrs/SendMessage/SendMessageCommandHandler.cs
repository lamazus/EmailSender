using System.Text.Json;
using EmailSender.Infrastructure;
using EmailSender.Infrastructure.Models;
using EmailSender.Infrastructure.Services;
using MediatR;

namespace EmailSender.Cqrs.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly SmtpService smtpService;
        private readonly ApplicationContext context;

        public SendMessageCommandHandler(ApplicationContext context, SmtpService smtpService)
        {
            this.context = context;
            this.smtpService = smtpService;
        }

        public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                smtpService.SendMessage(request.Subject, request.Body, request.Recipients, cancellationToken);

                var message = new Message
                {
                    MessageId = Guid.NewGuid(),
                    Subject = request.Subject,
                    Body = request.Body,
                    Recipients = JsonSerializer.Serialize(request.Recipients),
                    Result = ResultEnum.OK.ToString(),
                    CreatedAt = DateTime.Now
                };
                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var message = new Message
                {
                    MessageId = Guid.NewGuid(),
                    Subject = request.Subject,
                    Body = request.Body,
                    Recipients = JsonSerializer.Serialize(request.Recipients),
                    Result = ResultEnum.Failed.ToString(),
                    FailedMessage = ex.Message,
                    CreatedAt = DateTime.Now
                };
                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }
        }
    }
}

