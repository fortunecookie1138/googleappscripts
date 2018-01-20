using Dapper;
using MarketingPostManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingPostManager.Web.Test.Integration
{
    public class IntegrationTestHelper
    {
        private const string _testManagerName = "IntegrationTestManager";
        private readonly string _marketingPostManagerConnection;

        public string TestPostHyperlink => "https://blog.com/testpost";
        public string TestPostDescription => "This is a test post";
        public string TestPostImagePath => "TestImagePath";
        public Group TestPostGroup => Group.Office;
        public List<string> TestPostTags => new List<string> { "TestTag1", "TestTag2" };

        public IntegrationTestHelper()
        {
            _marketingPostManagerConnection = ConfigurationManager.ConnectionStrings["MarketingPostManager"].ConnectionString;
        }

        public async Task<int> InsertTestPost()
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                var insertedPostId = await connection.ExecuteScalarAsync<int>("dbo.MarketingPostManager_pr_Post_Insert",
                    new
                    {
                        Hyperlink = TestPostHyperlink,
                        Description = TestPostDescription,
                        ImagePath = TestPostImagePath,
                        CreatedBy = _testManagerName
                    }, commandType: CommandType.StoredProcedure);
                
                await connection.ExecuteAsync("dbo.MarketingPostManager_pr_PostGroup_Insert",
                    new
                    {
                        PostId = insertedPostId,
                        GroupId = TestPostGroup
                    }, commandType: CommandType.StoredProcedure);

                return insertedPostId;
            }
        }

        public async Task<int> SelectTableCount(TableName tableName)
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                return await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM {Enum.GetName(typeof(TableName), tableName)} WITH (NOLOCK)");
            }
        }

        public async Task<Post> SelectPost(int postId)
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                return await connection.QueryFirstOrDefaultAsync<Post>
                    ($"SELECT PostId, Hyperlink, [Description], ImagePath FROM MarketingPostManager_ent_Post WITH (NOLOCK) WHERE PostId = {postId}");
            }
        }

        public async Task<List<int>> SelectPostGroups(int postId)
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                return (await connection.QueryAsync<int>($"SELECT GroupId FROM MarketingPostManager_xref_PostGroup WITH (NOLOCK) WHERE PostId = {postId}")).ToList();
            }
        }

        public async Task<int> SelectTagId(string tagName)
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                return (await connection.QuerySingleAsync<int>($"SELECT TagId FROM MarketingPostManager_ent_Tag WITH (NOLOCK) WHERE TagName = '{tagName}'"));
            }
        }

        public async Task DeleteAllPosts()
        {
            using (var connection = new SqlConnection(_marketingPostManagerConnection))
            {
                await connection.ExecuteAsync($"DELETE FROM [dbo].[MarketingPostManager_xref_PostTag]");
                await connection.ExecuteAsync($"DELETE FROM [dbo].[MarketingPostManager_xref_PostGroup]");
                await connection.ExecuteAsync($"DELETE FROM [dbo].[MarketingPostManager_ent_Post]");
            }
        }
    }

    public enum TableName
    {
        MarketingPostManager_ent_Post = 1,
        MarketingPostManager_ent_Tag = 2,
        MarketingPostManager_enu_Group = 3,
        MarketingPostManager_xref_PostTag = 4,
        MarketingPostManager_xref_PostGroup = 5
    }
}
