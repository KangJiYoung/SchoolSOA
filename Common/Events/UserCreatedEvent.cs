using System;

namespace Common.Events
{
    public class UserCreatedEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        
        public UserCreatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}