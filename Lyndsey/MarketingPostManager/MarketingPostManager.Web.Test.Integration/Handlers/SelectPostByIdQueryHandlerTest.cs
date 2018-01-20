using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.CQRS.Queries;
using Ninject;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class SelectPostByIdQueryHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly SelectPostByIdQueryHandler _handler = DiContainer.Container.Get<SelectPostByIdQueryHandler>();

        [Fact, AutoRollback]
        public async Task Handle_PostExists_ReturnsPost()
        {
            var createdPostId = await _testHelper.InsertTestPost();

            var result = await _handler.Handle(new SelectPostByIdQuery(createdPostId));

            Assert.NotNull(result);
            Assert.True(result.Description.Equals(_testHelper.TestPostDescription) 
                && result.Hyperlink.Equals(_testHelper.TestPostHyperlink));
        }

        [Fact, AutoRollback]
        public async Task Handle_PostDoesNotExist_ReturnsNull()
        {
            var result = await _handler.Handle(new SelectPostByIdQuery(-1));

            Assert.Null(result);
        }
    }
}
