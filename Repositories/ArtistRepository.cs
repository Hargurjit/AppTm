using AppTm.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly Context _context;

        public ArtistRepository(Context context)
        {
            _context = context;
        }

        public Artist GetById(int id)
        {
            return _context.Set<Artist>().Find(id);
        }

        public Artist GetByName(string name)
        {
            return _context.Set<Artist>().Where(a => a.Name == name).FirstOrDefault();
        }

        public List<Artist> GetAll()
        {
            return _context.Set<Artist>().ToList();
        }

        public void Update(Artist artist)
        {
            _context.Set<Artist>().Update(artist);
            _context.Entry(artist).State = EntityState.Modified;
        }
        public void Add(Artist artist)
        {
            _context.Add(artist);
        }

        public void AddRange(IEnumerable<Artist> artists)
        {
            _context.AddRange(artists);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Artist artist = GetById(id);
            _context.Artists.Remove(artist);
        }
    }
}
