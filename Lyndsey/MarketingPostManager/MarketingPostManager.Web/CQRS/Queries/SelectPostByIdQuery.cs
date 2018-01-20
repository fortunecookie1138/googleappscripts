using MarketingPostManager.Web.Models;
using MediatR;

namespace MarketingPostManager.Web.CQRS.Queries
{
    /// <summary>
    /// Query for getting a single Post by its Id
    /// </summary>
    public class SelectPostByIdQuery : IAsyncRequest<Post>
    {
        public SelectPostByIdQuery(int postId)
        {
            PostId = postId;
        }

        public int PostId { get; }
    }
}