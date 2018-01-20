using MarketingPostManager.Web.CQRS.Commands;
using MarketingPostManager.Web.CQRS.Queries;
using MarketingPostManager.Web.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MarketingPostManager.Web.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IMediator _mediator;

        // todo add controller for getting all tags used on all posts in a group (tags by group?)

        /// <summary>
        /// Dependency injection constructor
        /// </summary>
        /// <param name="mediator">The mediator for sending messages</param>
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Inserts a new Post to the database
        /// </summary>
        /// <param name="postToCreate">The Post to create</param>
        /// <returns>The Id of the created Post</returns>
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> CreatePost([FromBody] FullPost postToCreate)
        {
            var createdPostId = await _mediator.SendAsync(new InsertPostCommand(postToCreate.Hyperlink, 
                postToCreate.Description, postToCreate.ImagePath, postToCreate.Tags, postToCreate.Groups));

            return Ok(createdPostId);
        }

        /// <summary>
        /// Gets all Posts within a certain Group
        /// </summary>
        /// <param name="groupId">The Group to filter Posts with</param>
        /// <returns>A collection of Posts filtered by Group, or NotFound if no Posts were found</returns>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IList<Post>))]
        public async Task<IHttpActionResult> GetPosts(Group groupId)
        {
            // TODO this definitely needs paging before I'm done
            var articles = await _mediator.SendAsync(new SelectPostsByGroupQuery(groupId));
            if (articles.Any() == false)
                return NotFound();
            else
                return Ok(articles);
        }

        /// <summary>
        /// Gets a Post by its Id
        /// </summary>
        /// <param name="postId">The Id of the Post to get</param>
        /// <returns>The Post if it exists, otherwise NotFound</returns>
        [Route("{postId}")]
        [HttpGet]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> GetPostById(int postId)
        {
            var post = await _mediator.SendAsync(new SelectPostByIdQuery(postId));
            if (post == null)
                return NotFound();
            else
                return Ok(post);
        }

        /// <summary>
        /// Updates all fields for the passed in Post
        /// </summary>
        /// <param name="postToUpdate">The Post to update</param>
        /// <returns>Http Ok</returns>
        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePostById([FromBody] FullPost postToUpdate)
        {
            await _mediator.SendAsync(new UpdatePostByIdCommand(postToUpdate.PostId, 
                postToUpdate.Hyperlink, postToUpdate.Description, postToUpdate.ImagePath, 
                postToUpdate.Tags, postToUpdate.Groups));

            return Ok();
        }

        /// <summary>
        /// Deletes the Post with the given Id
        /// </summary>
        /// <param name="postId">The Id of the Post to delete</param>
        /// <returns>Http Ok</returns>
        [Route("{postId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePostById(int postId)
        {
            await _mediator.SendAsync(new DeletePostByIdCommand(postId));

            return Ok();
        }
    }
}
