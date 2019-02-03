using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SchoolSOA.Services.Blog.Features
{
    public class BlogController : Controller
    {
        private readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<BlogsEnvelope> GetAll(
            [FromQuery] int skip,
            [FromQuery] int take)
        {
            return await mediator.Send(new List.Query(skip, take));
        }

        [HttpPost]
        [Authorize]
        public async Task Insert([FromBody] Insert.InsertBlogViewModel model)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            await mediator.Send(new Insert.Query(model.Title, model.Content, userId));
        }
    }
}