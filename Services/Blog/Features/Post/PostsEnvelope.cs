using System;
using System.Collections.Generic;
using SchoolSOA.Services.Blog;

namespace SchoolSOA.Services.Blog.Post
{
    public class PostsEnvelope
    {
        public IEnumerable<Blog.Entities.Post> Posts { get; }

        public PostsEnvelope(IEnumerable<Blog.Entities.Post> posts)
        {
            Posts = posts;
        }
    }
}