using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badge.Features.Badge
{
    public class BadgeController : Controller
    {
        private readonly IMediator mediator;

        public BadgeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<BadgesEnvelope> GetMyBadges()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return await mediator.Send(new List.Query(userId));
        }
    }
}