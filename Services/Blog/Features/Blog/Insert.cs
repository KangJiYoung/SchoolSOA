using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Events;
using MassTransit;
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
            private readonly IBus bus;

            public QueryHandler(BlogDbContext dbContext, IBus bus)
            {
                this.dbContext = dbContext;
                this.bus = bus;
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

                await bus.Publish(new UserPostedBlogEvent(request.CreatorId), cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}