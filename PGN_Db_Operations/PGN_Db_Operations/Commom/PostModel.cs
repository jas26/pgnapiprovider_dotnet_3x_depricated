using System;
namespace PGN_Db_Operations.Commom
{
    public class PostModel
    {
        public string post_id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}