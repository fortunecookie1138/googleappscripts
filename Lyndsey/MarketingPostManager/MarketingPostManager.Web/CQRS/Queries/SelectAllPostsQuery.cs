using MarketingPostManager.Web.Models;
using MediatR;
using System.Collections.Generic;

namespace MarketingPostManager.Web.CQRS.Queries
{
    public class SelectAllPostsQuery : IAsyncRequest<List<Post>>
    {
    }
}