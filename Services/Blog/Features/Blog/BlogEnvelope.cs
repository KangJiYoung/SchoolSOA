using System;
using System.Collections.Generic;

namespace SchoolSOA.Services.Blog.Features
{
    public class BlogsEnvelope
    {
        public IEnumerable<Blog.Entities.Blog> Blogs { get; }

        public BlogsEnvelope(IEnumerable<Blog.Entities.Blog> blogs)
        {
            Blogs = blogs;
        }
    }
}