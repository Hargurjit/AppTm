using AppTm.Entities;
using System.Collections.Generic;

namespace AppTm.Services
{
    public interface ISongService
    {
        List<Song> GetAll();

        Song GetSongByName(string name);

        bool Exists(SongDto song);

        void Delete(int id);

        void Update(int id, SongDto song);

        void AddSongs(IEnumerable<SongDto> songs);

        void Add(SongDto song);
    }
}