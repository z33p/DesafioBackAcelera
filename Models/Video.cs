using System;
using DesafioBack.Models.Shared;
using DesafioBack.Models.Tables;

namespace DesafioBack.Models
{
    public class Video : IEntity<Video>
    {
        public IDbTable<Video> DbTable => VideoTable.Instance;

        public long Id { get; set; }
        
        public string VideoId { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }

        public long Duration { get; set; }
        public DateTime PublishedAt { get; set; }

        public bool Deleted { get; set; } = false;
    }
}