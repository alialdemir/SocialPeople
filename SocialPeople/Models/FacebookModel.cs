using System.Collections.Generic;

namespace SocialPeople.Models
{

    public class Data
    {
        public int height { get; set; }
        public bool is_silhouette { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool is_verified { get; set; }
        public string link { get; set; }
        public Picture picture { get; set; }
    }

    public class Paging
    {
        public string next { get; set; }
    }

    public class RootObject
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }
}