using System;

namespace Common.Events
{
    public class UserPostedBlogEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        
        public UserPostedBlogEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}