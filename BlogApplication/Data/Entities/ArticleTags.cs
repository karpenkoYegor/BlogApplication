using System.Collections.Generic;

namespace BlogApplication.Data.Entities
{
    public class ArticleTags
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
