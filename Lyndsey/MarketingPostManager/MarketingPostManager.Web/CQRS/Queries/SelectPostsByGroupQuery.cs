using MarketingPostManager.Web.Models;
using MediatR;
using System.Collections.Generic;

namespace MarketingPostManager.Web.CQRS.Queries
{
    /// <summary>
    /// Query for getting all Posts in a Group
    /// </summary>
    public class SelectPostsByGroupQuery : IAsyncRequest<List<Post>>
    {
        public Group Group { get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="group">The group to filter posts by</param>
        public SelectPostsByGroupQuery(Group group)
        {
            Group = group;
        }
    }
}