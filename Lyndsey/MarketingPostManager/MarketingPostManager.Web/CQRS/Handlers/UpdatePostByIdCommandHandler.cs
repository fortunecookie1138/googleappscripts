using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Commands;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    /// <summary>
    /// Handler for updating a Post by its Id
    /// </summary>
    public class UpdatePostByIdCommandHandler : IAsyncRequestHandler<UpdatePostByIdCommand, Unit>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="connectionStringConfiguration">The connection string configuration provider</param>
        public UpdatePostByIdCommandHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Update a Post by its Id
        /// </summary>
        /// <param name="message">The message with Post and xref fields to update</param>
        /// <returns></returns>
        public async Task<Unit> Handle(UpdatePostByIdCommand message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                await connection.ExecuteAsync("[dbo].[MarketingPostManager_pr_Post_Update]",
                    new
                    {
                        PostId = message.PostId,
                        Hyperlink = message.Hyperlink,
                        Description = message.Description,
                        ImagePath = message.ImagePath,
                        UpdatedBy = "MarkingPostManager.Web" // todo get real user
                    }, commandType: CommandType.StoredProcedure);
            }

            return Unit.Value;
        }
    }
}