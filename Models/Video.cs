using System;
using System.Collections.Generic;
using DesafioBack.Models.Shared;
using DesafioBack.Models.Tables;

namespace DesafioBack.Models
{
    public class Video : IEntity<Video>
    {
        public IDbTable<Video> DbTable => VideoTable.Instance;

        public string VideoId { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }

        public int Duration { get; set; }
        public DateTime PublishedAt { get; set; }

    }
}