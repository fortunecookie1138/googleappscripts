using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.Models;
using Ninject;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class InsertPostGroupCommandHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly InsertPostGroupCommandHandler _handler = DiContainer.Container.Get<InsertPostGroupCommandHandler>();

        [Fact, AutoRollback]
        public async Task Handle_NominalCase_PostGroupIsInserted()
        {
            var createdPostId = await _testHelper.InsertTestPost();
            var initialPostGroupCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostGroup);

            var result = await _handler.Handle(new InsertPostGroupCommand(createdPostId, Group.Office));
          
            var finalPostGroupCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostGroup);
            var createdPostGroups = await _testHelper.SelectPostGroups(createdPostId);

            Assert.True(initialPostGroupCount < finalPostGroupCount);
            Assert.NotNull(createdPostGroups);
            Assert.Equal(2, createdPostGroups.Count);
            Assert.True(createdPostGroups.Any(pg => pg == (int)Group.Office));
        }
    }
}
