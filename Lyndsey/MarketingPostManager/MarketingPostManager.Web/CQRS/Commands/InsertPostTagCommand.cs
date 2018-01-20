using MediatR;

namespace MarketingPostManager.Web.CQRS.Commands
{
    public class InsertPostTagCommand : IAsyncRequest<Unit>
    {
        public InsertPostTagCommand(int postId, string tagName)
        {
            PostId = postId;
            TagName = tagName;
        }

        public int PostId { get; }

        public string TagName { get; }
    }
}