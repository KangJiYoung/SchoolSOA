using System.Threading.Tasks;
using Badge.Entities;
using Common.Events;
using MassTransit;

namespace Badge.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        public BadgeDbContext DbContext { get; }

        public UserCreatedConsumer(BadgeDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            await DbContext.AddRangeAsync(
                new Entities.Badge (context.Message.UserId, BadgeType.Beginner),
                new Entities.Badge (context.Message.UserId, BadgeType.Expert));
            await DbContext.SaveChangesAsync();
        }
    }
}