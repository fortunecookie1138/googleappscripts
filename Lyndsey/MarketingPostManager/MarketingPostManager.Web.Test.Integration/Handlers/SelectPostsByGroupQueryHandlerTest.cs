using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.CQRS.Queries;
using MarketingPostManager.Web.Models;
using Ninject;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class SelectPostsByGroupQueryHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly SelectPostsByGroupQueryHandler _handler = DiContainer.Container.Get<SelectPostsByGroupQueryHandler>();
        
        [Fact, AutoRollback]
        public async Task Handle_PostsExistForGroup_ReturnsPosts()
        {
            await _testHelper.InsertTestPost();

            var result = await _handler.Handle(new SelectPostsByGroupQuery(_testHelper.TestPostGroup));

            Assert.NotEmpty(result);
            Assert.True(result.Any(p => p.Description.Equals(_testHelper.TestPostDescription) 
                                        && p.Hyperlink.Equals(_testHelper.TestPostHyperlink)));
        }

        [Fact, AutoRollback]
        public async Task Handle_PostsDoNotExistForGroup_ReturnsEmptyCollection()
        {
            await _testHelper.InsertTestPost();

            var result = await _handler.Handle(new SelectPostsByGroupQuery(Group.Search));

            Assert.Empty(result);
        }
    }
}
