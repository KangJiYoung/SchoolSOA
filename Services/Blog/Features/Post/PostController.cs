using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}