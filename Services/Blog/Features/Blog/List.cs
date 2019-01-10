using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSOA.Services.Blog.Entities;

namespace SchoolSOA.Services.Blog.Features
{
    public class List
    {
        public class Query : IRequest<BlogsEnvelope>
        {
            public int Skip { get; }
            public int Take { get; }

            public Query(int skip, int take)
            {
                Skip = skip;
                Take = take;
            }
        }

        public class QueryHandler : IRequestHandler<Query, BlogsEnvelope>
        {
            private readonly BlogDbContext dbContext;
            private readonly ILogger<QueryHandler> logger;

            public QueryHandler(BlogDbContext dbContext, ILogger<QueryHandler> logger)
            {
                this.dbContext = dbContext;
                this.logger = logger;
            }

            public async Task<BlogsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogs = await dbContext
                    .Blogs
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                logger.LogCritical((await dbContext.Blogs.CountAsync()).ToString());

                return new BlogsEnvelope(blogs);
            }
        }
    }
}