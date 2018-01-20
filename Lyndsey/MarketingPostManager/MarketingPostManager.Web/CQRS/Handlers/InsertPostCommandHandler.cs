using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    /// <summary>
    /// Handler for inserting a Post
    /// </summary>
    public class InsertPostCommandHandler : IAsyncRequestHandler<InsertPostCommand, int>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="configuration">The connection string configuration provider</param>
        /// <param name="mediator">The Mediator to use for sending messages</param>
        public InsertPostCommandHandler(IConnectionStringConfiguration configuration, IMediator mediator)
        {
            _connectionStringConfiguration = configuration;
            _mediator = mediator;
        }

        /// <summary>
        /// Inserts a Post
        /// </summary>
        /// <param name="message">The message with Post properties to insert</param>
        /// <returns>The Id of the created Post</returns>
        public async Task<int> Handle(InsertPostCommand message)
        {
            var createdByTemp = "MarkingPostManager.Web"; // todo get real user
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                var createdPostId = await connection.ExecuteScalarAsync<int>("[dbo].[MarketingPostManager_pr_Post_Insert]",
                    new
                    {
                        Hyperlink = message.Hyperlink,
                        Description = message.Description,
                        ImagePath = message.ImagePath,
                        CreatedBy = createdByTemp
                    }, commandType: CommandType.StoredProcedure);

                foreach(var tag in message.Tags)
                {
                    await _mediator.SendAsync(new InsertPostTagCommand(createdPostId, tag));
                }

                foreach(var group in message.Groups)
                {
                    await _mediator.SendAsync(new InsertPostGroupCommand(createdPostId, group));
                }

                return createdPostId;
            }
        }
    }
}