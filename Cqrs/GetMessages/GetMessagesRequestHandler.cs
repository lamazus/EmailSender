using System.Text.Json;
using AutoMapper;
using EmailSender.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmailSender.Cqrs.GetMessages
{
    public class GetMessagesRequestHandler : IRequestHandler<GetMessagesRequest, List<MessagesResponse>>
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public GetMessagesRequestHandler(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<MessagesResponse>> Handle(GetMessagesRequest request, CancellationToken cancellationToken)
        {
            var response = await context.Messages.ToListAsync();
            var messages = mapper.Map<List<MessagesResponse>>(response);
            if(response.Count > 0)
                for(int i = 0; i < messages.Count; i++)
                    messages[i].Recipients = JsonSerializer.Deserialize<List<string>>(response[i].Recipients)!;

            return messages;
        }
    }
}