using Dapper;
using MarketingPostManager.Web.Configuration;
using MarketingPostManager.Web.CQRS.Queries;
using MarketingPostManager.Web.Models;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.CQRS.Handlers
{
    public class SelectAllPostsQueryHandler : IAsyncRequestHandler<SelectAllPostsQuery, List<Post>>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="configuration">The connection string configuration provider</param>
        public SelectAllPostsQueryHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }
        
        /// <summary>
        /// Selects all Posts
        /// </summary>
        /// <param name="message">The message to handle</param>
        /// <returns>A collection of all Posts</returns>
        public async Task<List<Post>> Handle(SelectAllPostsQuery message)
        {
            // TODO this currently isn't being used
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                return (await connection.QueryAsync<Post>("[dbo].[MarketingPostManager_pr_Post_Select]", 
                    commandType: CommandType.StoredProcedure)).ToList();
            }
        }
    }
}