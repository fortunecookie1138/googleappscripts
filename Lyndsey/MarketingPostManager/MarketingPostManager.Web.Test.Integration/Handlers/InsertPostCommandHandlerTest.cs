using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.Models;
using Ninject;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class InsertPostCommandHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly InsertPostCommandHandler _handler = DiContainer.Container.Get<InsertPostCommandHandler>();
        
        [Fact, AutoRollback]
        public async Task Handle_NewPostToInsert_PostIsInserted()
        {
            var initialPostCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Post);
            var initialPostTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostTag);
            var initialPostGroupCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostGroup);

            var createdPostId = await _handler.Handle(new InsertPostCommand(_testHelper.TestPostHyperlink, 
                    _testHelper.TestPostDescription, _testHelper.TestPostImagePath, _testHelper.TestPostTags,
                    new List<Group> { _testHelper.TestPostGroup }));

            var finalPostCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Post);
            var finalPostTagCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostTag);
            var finalPostGroupCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_xref_PostGroup);
            var createdPost = await _testHelper.SelectPost(createdPostId);

            Assert.True(initialPostCount < finalPostCount);
            Assert.True(initialPostTagCount < finalPostTagCount);
            Assert.True(initialPostGroupCount < finalPostGroupCount);
            Assert.NotNull(createdPost);
            Assert.True(createdPost.Hyperlink.Equals(_testHelper.TestPostHyperlink) 
                        && createdPost.Description.Equals(_testHelper.TestPostDescription)
                        && createdPost.ImagePath.Equals(_testHelper.TestPostImagePath));
        }
    }
}
