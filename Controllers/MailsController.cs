using Microsoft.AspNetCore.Mvc;
using MediatR;
using EmailSender.Cqrs.SendMessage;
using EmailSender.Cqrs.GetMessages;
using EmailSender.Infrastructure.Models;
using System.Text.Json;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Получить список всех сообщений
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<MessagesResponse>> GetAllMessages(CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetMessagesRequest(), cancellationToken);
        }

        /// <summary>
        /// Отправить сообщение на указанные электронные ящики
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task SendMessage([FromBody] SendMessageCommand message, CancellationToken cancellationToken)
        {
            await mediator.Send(message, cancellationToken);
        }
    }
}