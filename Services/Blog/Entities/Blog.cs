using System;
using System.Collections.Generic;

namespace SchoolSOA.Services.Blog.Entities
{
    public class Blog
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatorName { get; set; }

        public DateTime Created { get; set; }
    }
}