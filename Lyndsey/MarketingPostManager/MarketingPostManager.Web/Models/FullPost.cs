using System.Collections.Generic;

namespace MarketingPostManager.Web.Models
{
    public class FullPost
    {
        public int PostId { get; set; }

        public string Hyperlink { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public List<string> Tags { get; set; }

        public List<Group> Groups { get; set; }
    }
}