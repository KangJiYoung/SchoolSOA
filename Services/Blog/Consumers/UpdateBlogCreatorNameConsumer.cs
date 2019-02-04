using System.Threading.Tasks;
using Common.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSOA.Services.Blog.Entities;

namespace Blog.Consumers
{
    public class UpdateBlogCreatorNameConsumer : IConsumer<UserChangedFullNameEvent>
    {
        private readonly BlogDbContext dbContext;

        public UpdateBlogCreatorNameConsumer(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<UserChangedFullNameEvent> context)
        {
            using (var connection = dbContext.Database.GetDbConnection()) {
                await connection.OpenAsync();     
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $@"
                        UPDATE Blogs
                        SET CreatorName = '{context.Message.NewFullName}'
                        WHERE CreatorId = '{context.Message.Guid}'";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}