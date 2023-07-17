using AutoMapper;
using EmailSender.Cqrs.GetMessages;
using EmailSender.Infrastructure.Models;

namespace EmailSender.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Message, MessagesResponse>().ForMember(x => x.Recipients, a => a.Ignore());
        }
    }
}