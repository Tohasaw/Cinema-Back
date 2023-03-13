using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Countries { get; set; }
        public string Genres { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public string AgeLimit { get; set; }
        public string Description { get; set; }
        public string VideoLink { get; set; }
        public string Image { get; set; }
        public ICollection<TableEntry> TableEntries { get; set; } = new List<TableEntry>();
    }
}
