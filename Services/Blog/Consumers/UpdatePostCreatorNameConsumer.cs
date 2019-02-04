using System.Threading.Tasks;
using Common.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SchoolSOA.Services.Blog.Entities;

namespace Blog.Consumers
{
    public class UpdatePostCreatorNameConsumer : IConsumer<UserChangedFullNameEvent>
    {
        private readonly BlogDbContext dbContext;

        public UpdatePostCreatorNameConsumer(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task Consume(ConsumeContext<UserChangedFullNameEvent> context)
        {
            using (var connection = dbContext.Database.GetDbConnection()) {
                await connection.OpenAsync();     
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $@"
                        UPDATE Posts
                        SET CreatorName = '{context.Message.NewFullName}'
                        WHERE CreatorId = '{context.Message.Guid}'";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}