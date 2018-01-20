using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Handlers;
using Ninject;
using System.Threading.Tasks;
using Xunit;

namespace MarketingPostManager.Web.Test.Integration.Handlers
{
    public class DeletePostByIdCommandHandlerTest
    {
        private readonly IntegrationTestHelper _testHelper = new IntegrationTestHelper();
        private readonly DeletePostByIdCommandHandler _handler = DiContainer.Container.Get<DeletePostByIdCommandHandler>();

        [Fact, AutoRollback]
        public async Task Handle_PostExists_DeletesPost()
        {
            var postId = await _testHelper.InsertTestPost();

            await _handler.Handle(new DeletePostByIdCommand(postId));

            var post = await _testHelper.SelectPost(postId);

            Assert.Null(post);
        }
    }
}
