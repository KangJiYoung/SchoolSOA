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

            public QueryHandler(BlogDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<BlogsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogs = await dbContext
                    .Blogs
                    .OrderByDescending(it => it.Created)
                    .Skip(request.Skip)
                    .Take(request.Take)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new BlogsEnvelope(blogs);
            }
        }
    }
}