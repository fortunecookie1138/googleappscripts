using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Queries;
using MarketingPostManager.Web.Models;
using MediatR;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    public class SelectPostByIdQueryHandler : IAsyncRequestHandler<SelectPostByIdQuery, Post>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="configuration">The connection string configuration provider</param>
        public SelectPostByIdQueryHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Gets a Post by its Id
        /// </summary>
        /// <param name="message">The message with the PostId</param>
        /// <returns>The Post if it exists, otherwise null</returns>
        public async Task<Post> Handle(SelectPostByIdQuery message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                return await connection.QueryFirstOrDefaultAsync<Post>("[dbo].[MarketingPostManager_pr_Post_Select]",
                    new
                    {
                        PostId = message.PostId
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}