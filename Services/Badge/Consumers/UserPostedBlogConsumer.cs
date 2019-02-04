using System.Linq;
using System.Threading.Tasks;
using Badge.Entities;
using Common.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Badge.Consumers
{
    public class UserPostedBlogConsumer : IConsumer<UserPostedBlogEvent>
    {
        private readonly BadgeDbContext dbContext;

        public UserPostedBlogConsumer(BadgeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<UserPostedBlogEvent> context)
        {
            var badges = await dbContext.Badges.Where(it => it.UserId == context.Message.UserId).ToListAsync();

            foreach (var badge in badges)
                badge.CurrentThreshold++;
            
            dbContext.UpdateRange(badges);
            await dbContext.SaveChangesAsync();
        }
    }
}