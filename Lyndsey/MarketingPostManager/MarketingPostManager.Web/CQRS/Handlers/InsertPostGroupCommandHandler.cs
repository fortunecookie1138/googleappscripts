using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    public class InsertPostGroupCommandHandler : IAsyncRequestHandler<InsertPostGroupCommand, Unit>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        /// <summary>
        /// Dependency injection constructor
        /// </summary>
        /// <param name="configuration">The connection string configuration</param>
        public InsertPostGroupCommandHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Inserts a PostGroup xref record
        /// </summary>
        /// <param name="message">The message with values to insert</param>
        /// <returns>Mediatr Unit</returns>
        public async Task<Unit> Handle(InsertPostGroupCommand message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                await connection.ExecuteAsync("[dbo].[MarketingPostManager_pr_PostGroup_Insert]",
                        new
                        {
                            PostId = message.PostId,
                            GroupId = message.Group
                        }, commandType: CommandType.StoredProcedure);
            }
            return Unit.Value;
        }
    }
}