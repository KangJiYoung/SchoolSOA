using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolSOA.Services.Post.Features;

namespace SchoolSOA.Services.Blog.Post
{
    public class PostController : Controller
    {
        private readonly IMediator mediator;

        public PostController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<PostsEnvelope> GetAll(
            [FromQuery] Guid blogId)
        {
            return await mediator.Send(new List.Query(blogId));
        }

        [HttpPost]
        public Task<Entities.Post> Insert([FromBody] Insert.InsertPostViewModel model)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return mediator.Send(new Insert.Query(model.BlogId, userId, model.Content));
        }
    }
}