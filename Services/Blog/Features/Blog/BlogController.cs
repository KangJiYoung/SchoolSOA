using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}