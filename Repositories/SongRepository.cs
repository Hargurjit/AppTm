using AppTm.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private readonly Context _context;

        public SongRepository(Context context)
        {
            _context = context;
        }

        public Song GetById(int id)
        {
            return _context.Set<Song>().Find(id);
        }

        public Song GetByName(string name)
        {
            return _context.Set<Song>().Where(s => s.Name == name).FirstOrDefault();
        }

        public List<Song> GetAll()
        {
            return _context.Set<Song>().ToList();
        }

        public void Update(Song song)
        {
            _context.Set<Song>().Update(song);
            _context.Entry(song).State = EntityState.Modified;
        }
        public void Add(Song song)
        {
            _context.Add(song);
        }

        public void AddRange(IEnumerable<Song> songs)
        {
            _context.AddRange(songs);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Song song = GetById(id);
            _context.Songs.Remove(song);
        }
    }
}
