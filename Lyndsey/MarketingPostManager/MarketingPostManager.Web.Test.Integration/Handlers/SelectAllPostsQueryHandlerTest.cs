using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.CQRS.Queries;
using Ninject;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class SelectAllPostsQueryHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly SelectAllPostsQueryHandler _handler = DiContainer.Container.Get<SelectAllPostsQueryHandler>();

        [Fact, AutoRollback]
        public async Task Handle_PostsExist_ReturnsPosts()
        {
            var createdPostId = await _testHelper.InsertTestPost();

            var result = await _handler.Handle(new SelectAllPostsQuery());

            Assert.NotNull(result);
            Assert.True(result.Any(p => p.PostId == createdPostId));
        }
    }
}
