using MarketingPostManager.Web.Models;
using MediatR;
using System.Collections.Generic;

namespace MarketingPostManager.Web.CQRS.Commands
{
    /// <summary>
    /// Command to update a Post by its Id
    /// </summary>
    public class UpdatePostByIdCommand : IAsyncRequest<Unit>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="postId">The Id of the Post to update</param>
        /// <param name="hyperlink">The hyperlink to the post</param>
        /// <param name="description">A short description of the post</param>
        /// <param name="imagePath">The path to the image for the post</param>
        /// <param name="tags">The tags to associate with the Post</param>
        /// <param name="groups">The Groups that the Post belongs to</param>
        public UpdatePostByIdCommand(int postId, string hyperlink, string description, string imagePath, List<string> tags, List<Group> groups)
        {
            PostId = postId;
            Hyperlink = hyperlink;
            Description = description;
            ImagePath = imagePath;
            Tags = tags;
            Groups = groups;
        }

        /// <summary>
        /// The Id of the Post to update
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// The hyperlink to the post
        /// </summary>
        public string Hyperlink { get; }

        /// <summary>
        /// A short description of the post
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The path to the image for the post
        /// </summary>
        public string ImagePath { get; }

        /// <summary>
        /// The tags to associate with the Post
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// The Groups that the Post belongs to
        /// </summary>
        public List<Group> Groups { get; set; }
    }
}