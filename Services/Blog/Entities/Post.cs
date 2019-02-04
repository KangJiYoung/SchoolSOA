using System;

namespace SchoolSOA.Services.Blog.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }

        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }

        public string Content { get; set; }
        public string CreatorName { get; set; }
    }
}