using AppTm.Entities;
using AppTm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Services
{
    public class ArtistService : IArtistService
    {
        IRepository<Artist> _artistRepo;
        public ArtistService(IRepository<Artist> artistRepo)
        {
            _artistRepo = artistRepo;
        }
        public List<Artist> GetAll()
        {
            return _artistRepo.GetAll();
        }

        public Artist GetArtistByName (string name)
        {
            return _artistRepo.GetByName(name); 
        }

        public bool Exists(Artist artist)
        {
            return _artistRepo.GetByName(artist.Name) != null && _artistRepo.GetById(artist.Id) != null;
        }

        public void Delete(int id)
        {
            if (_artistRepo.GetById(id) == null) throw new KeyNotFoundException("Given Id not correct!");
            _artistRepo.Delete(id);
            _artistRepo.SaveChanges();
        }

        public void Update(int id, Artist artist)
        {
            var findArtist = _artistRepo.GetById(id);
            if (findArtist == null) throw new KeyNotFoundException("Given Id not correct!");
            else if (artist.Name == "") throw new ArgumentNullException("Name not given");
            else if (_artistRepo.GetByName(artist.Name) != null) throw new ArgumentException("Person with Name '" + artist.Name + "' already exists!");
            else
            {
                findArtist.Name = artist.Name ?? findArtist.Name;
                _artistRepo.Update(findArtist);
                _artistRepo.SaveChanges();
            }
        }

        public void AddArtists(IEnumerable<Artist> artists)
        {
            _artistRepo.AddRange(artists.ToList());
            _artistRepo.SaveChanges();
        }

        public void Add(Artist artist)
        {
            if (artist.Name == "") throw new ArgumentNullException("Artist Name empty for id " + artist.Id.ToString());
            else if (Exists(artist)) throw new ArgumentException("Artist with Name " + artist.Name + " and Id " + artist.Id.ToString() + " already exists");
            else {
                _artistRepo.Add(artist);
                _artistRepo.SaveChanges();
            }
        }
    }
}
