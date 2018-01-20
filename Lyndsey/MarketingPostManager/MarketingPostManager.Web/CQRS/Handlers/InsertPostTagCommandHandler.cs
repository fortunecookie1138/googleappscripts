using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Commands;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    public class InsertPostTagCommandHandler : IAsyncRequestHandler<InsertPostTagCommand, Unit>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        public InsertPostTagCommandHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Inserts a PostTag xref, and the Tag itself if it doesn't exist
        /// </summary>
        /// <param name="message">The message with the information to insert</param>
        /// <returns>Mediatr Unit</returns>
        public async Task<Unit> Handle(InsertPostTagCommand message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                await connection.ExecuteAsync("[dbo].[MarketingPostManager_pr_PostTag_Insert]",
                        new
                        {
                            PostId = message.PostId,
                            TagName = message.TagName,
                            CreatedBy = "MarkingPostManager.Web" // todo get real user
                        }, commandType: CommandType.StoredProcedure);
            }
            return Unit.Value;
        }
    }
}