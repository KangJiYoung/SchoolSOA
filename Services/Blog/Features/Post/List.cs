using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSOA.Services.Blog.Entities;

namespace SchoolSOA.Services.Blog.Post
{
    public class List
    {
        public class Query : IRequest<PostsEnvelope>
        {
            public Guid BlogId { get; }

            public Query(Guid blogId)
            {
                BlogId = blogId;
            }
        }

        public class QueryHandler : IRequestHandler<Query, PostsEnvelope>
        {
            private readonly BlogDbContext dbContext;

            public QueryHandler(BlogDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<PostsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogs = await dbContext
                    .Posts
                    .Where(it => it.BlogId == request.BlogId)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new PostsEnvelope(blogs);
            }
        }
    }
}