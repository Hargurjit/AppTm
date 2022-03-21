using AppTm.Entities;
using System.Collections.Generic;

namespace AppTm.Services
{
    public interface IArtistService
    {
        List<Artist> GetAll();

        Artist GetArtistByName(string name);

        bool Exists(Artist artist);

        void Delete(int id);

        void Update(int id, Artist artist);

        void AddArtists(IEnumerable<Artist> artists);

        void Add(Artist artist);
    }
}