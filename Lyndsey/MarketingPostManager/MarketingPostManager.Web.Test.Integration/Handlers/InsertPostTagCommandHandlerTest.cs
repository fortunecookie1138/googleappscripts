using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Handlers;
using Ninject;
using Ploeh.AutoFixture.Xunit2;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class InsertPostTagCommandHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly InsertPostTagCommandHandler _handler = DiContainer.Container.Get<InsertPostTagCommandHandler>();

        [Theory, AutoData, AutoRollback]
        public async Task Handle_NewTag_TagInsertedWithPostTag(string tagName)
        {
            var createdPostId = await _testHelper.InsertTestPost();
            var initialPostTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostTag);
            var initialTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Tag);

            await _handler.Handle(new InsertPostTagCommand(createdPostId, tagName));

            var finalPostTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostGroup);
            var finalTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Tag);
            var createdTagId = await _testHelper.SelectTagId(tagName);

            Assert.True(initialPostTagCount < finalPostTagCount);
            Assert.True(initialTagCount < finalTagCount);
            Assert.NotNull(createdTagId);
        }
    }
}
