using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Handlers;
using MarketingPostManager.Web.Models;
using Ninject;
using Ploeh.AutoFixture.Xunit2;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class UpdatePostByIdCommandHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly UpdatePostByIdCommandHandler _handler = DiContainer.Container.Get<UpdatePostByIdCommandHandler>();

        [Theory, AutoData, AutoRollback]
        public async Task Handle_PostExists_PostIsUpdated(UpdatePostByIdCommand postFields)
        {
            var createdPostId = await _testHelper.InsertTestPost();
            var command = new UpdatePostByIdCommand(createdPostId, postFields.Hyperlink,
                postFields.Description, postFields.ImagePath, postFields.Tags, postFields.Groups);

            await _handler.Handle(command);

            var updatedPost = await _testHelper.SelectPost(createdPostId);
            Assert.True(postFields.Hyperlink.Equals(updatedPost.Hyperlink)
                && postFields.Description.Equals(updatedPost.Description)
                && postFields.ImagePath.Equals(updatedPost.ImagePath));
        }

        [Theory, AutoData, AutoRollback]
        public async Task Handle_PostDoesNotExist_DoesNothing(string junkString, List<string> junkTags, 
            List<Group> junkGroups)
        {
            var initialPostCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Post);

            await _handler.Handle(new UpdatePostByIdCommand(-1, junkString, junkString, junkString,
                junkTags, junkGroups));

            var finalPostCount = await _testHelper.SelectTableCount(TableName.MarketingPostManager_ent_Post);

            Assert.Equal(initialPostCount, finalPostCount);
        }
    }
}
