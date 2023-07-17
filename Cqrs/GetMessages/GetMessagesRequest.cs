using MediatR;

namespace EmailSender.Cqrs.GetMessages
{
    public class GetMessagesRequest : IRequest<List<MessagesResponse>>
    {
        
    }
}