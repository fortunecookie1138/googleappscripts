using MarketingPostManager.Web.Models;
using MediatR;

namespace MarketingPostManager.Web.CQRS.Commands
{
    /// <summary>
    /// Command for inserting a PostGroup xref
    /// </summary>
    public class InsertPostGroupCommand : IAsyncRequest<Unit>
    {
        public InsertPostGroupCommand(int postId, Group group)
        {
            PostId = postId;
            Group = group;
        }

        public int PostId { get; }

        public Group Group { get; }
    }
}