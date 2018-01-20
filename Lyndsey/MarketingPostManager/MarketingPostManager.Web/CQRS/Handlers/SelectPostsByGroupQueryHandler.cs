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
    /// <summary>
    /// Handler for getting all Posts in a Group
    /// </summary>
    public class SelectPostsByGroupQueryHandler : IAsyncRequestHandler<SelectPostsByGroupQuery, List<Post>>
    {
        private readonly IConnectionStringConfiguration _connectionStringConfiguration;
        
        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="configuration">The connection string configuration provider</param>
        public SelectPostsByGroupQueryHandler(IConnectionStringConfiguration configuration)
        {
            _connectionStringConfiguration = configuration;
        }

        /// <summary>
        /// Gets all Posts in a Group
        /// </summary>
        /// <param name="message">The message with Group to filter Posts by</param>
        /// <returns>A collection of Posts</returns>
        public async Task<List<Post>> Handle(SelectPostsByGroupQuery message)
        {
            using (var connection = new SqlConnection(_connectionStringConfiguration.MarketingPostManager))
            {
                return (await connection.QueryAsync<Post>("dbo.MarketingPostManager_pr_Post_Select_ByGroupId", 
                    new
                    {
                        GroupId = message.Group
                    }, commandType: CommandType.StoredProcedure)).ToList();
            }
        }
    }
}