using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Entities
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Artist { get; set; }
        public string Shortname { get; set; }
        public int? BPM { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string Album { get; set; }
    }
}
