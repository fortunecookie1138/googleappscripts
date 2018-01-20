using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    public class DeletePostByIdCommandHandler : IAsyncRequestHandler<DeletePostByIdCommand, Unit>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="connectionStringConfiguration">The connection string configuration provider</param>
        public DeletePostByIdCommandHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Deletes the Post with the given Id
        /// </summary>
        /// <param name="message">The Id of the Post to delete</param>
        /// <returns>Mediatr Unit</returns>
        public async Task<Unit> Handle(DeletePostByIdCommand message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                await connection.ExecuteAsync("dbo.MarketingPostManager_pr_Post_Delete",
                    new
                    {
                        PostId = message.PostId
                    }, commandType: CommandType.StoredProcedure);
            }

            return Unit.Value;
        }
    }
}