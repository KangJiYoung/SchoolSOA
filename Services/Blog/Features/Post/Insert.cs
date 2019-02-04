using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SchoolSOA.Services.Blog.Entities;

namespace SchoolSOA.Services.Post.Features
{
    public class Insert
    {
        public class Query : IRequest<Blog.Entities.Post>
        {
            public Guid BlogId { get; }
            public string CreatorName { get; }
            public string Content { get; }
            public Guid CreatorId { get; }

            public Query(Guid blogId, string creatorName, Guid creatorId, string content)
            {
                BlogId = blogId;
                CreatorName = creatorName;
                CreatorId = creatorId;
                Content = content;
            }
        }
        
        public class InsertPostViewModel
        {
            public Guid BlogId { get; set; }
            public string Content { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Blog.Entities.Post>
        {
            private readonly BlogDbContext dbContext;

            public QueryHandler(BlogDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Blog.Entities.Post> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = new Blog.Entities.Post
                {
                    CreatorId = request.CreatorId,
                    Content = request.Content,
                    BlogId = request.BlogId,
                    CreatorName = request.CreatorName
                };

                await dbContext.AddAsync(post, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
                
                return post;
            }
        }
    }
}