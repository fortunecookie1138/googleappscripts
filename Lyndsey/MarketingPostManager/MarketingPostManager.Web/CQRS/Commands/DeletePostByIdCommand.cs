using MediatR;

namespace MarketingPostManager.Web.CQRS.Commands
{
    /// <summary>
    /// Command for deleting a Post with the given Id
    /// </summary>
    public class DeletePostByIdCommand : IAsyncRequest<Unit>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="postId">The Id of the Post to delete</param>
        public DeletePostByIdCommand(int postId)
        {
            PostId = postId;
        }

        /// <summary>
        /// The Id of the Post to delete
        /// </summary>
        public int PostId { get; }
    }
}