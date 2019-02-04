using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SchoolSOA.Services.Blog.Entities;

namespace SchoolSOA.Services.Blog.Features
{
    public class Insert
    {
        public class Query : IRequest<Unit>
        {
            public string Title { get; }
            public string Content { get; }
            public string CreatorName { get; }
            public Guid CreatorId { get; }
            
            public Query(string title, string content, string creatorName, Guid creatorId)
            {
                Title = title;
                Content = content;
                CreatorName = creatorName;
                CreatorId = creatorId;
            }
        }
        
        public class InsertBlogViewModel
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Unit>
        {
            private readonly BlogDbContext dbContext;

            public QueryHandler(BlogDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                var blog = new Entities.Blog
                {
                    Content = request.Content,
                    Title = request.Title,
                    CreatorId = request.CreatorId,
                    CreatorName = request.CreatorName,
                    Created = DateTime.Now
                };

                await dbContext.AddAsync(blog, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}