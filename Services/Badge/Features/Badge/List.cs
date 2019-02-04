using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Badge.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Badge.Features.Badge
{
    public class List
    {
        public class Query : IRequest<BadgesEnvelope>
        {
            public Guid UserId { get; }

            public Query(Guid userId)
            {
                UserId = userId;
            }
        }

        public class QueryHandler : IRequestHandler<Query, BadgesEnvelope>
        {
            private readonly BadgeDbContext dbContext;

            public QueryHandler(BadgeDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<BadgesEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var badges = await dbContext
                    .Badges
                    .Where(it => it.UserId == request.UserId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new BadgesEnvelope(badges);
            }
        }
    }
}