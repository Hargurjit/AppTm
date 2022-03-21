using AppTm.Entities;
using AppTm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Services
{
    public class SongService : ISongService
    {
        IRepository<Song> _songRepo;
        IRepository<Artist> _artistRepo;

        public SongService(IRepository<Song> songRepo, IRepository<Artist> artistRepo)
        {
            _songRepo = songRepo;
            _artistRepo = artistRepo;
        }
        public List<Song> GetAll()
        {
            return _songRepo.GetAll();
        }

        public Song GetSongByName(string name)
        {
            return _songRepo.GetByName(name);
        }

        public bool Exists(SongDto song)
        {
            return _songRepo.GetByName(song.Name) != null || _songRepo.GetById(song.Id) != null;
        }

        public void Delete(int id)
        {
            if (_songRepo.GetById(id) == null) throw new KeyNotFoundException("Given Id not correct!");
            _songRepo.Delete(id);
            _songRepo.SaveChanges();
        }

        public void Update(int id, SongDto song)
        {
            var findArtist = _songRepo.GetById(id);
            if (findArtist == null) throw new KeyNotFoundException("Given Id not correct!");
            else if (song.Name == "") throw new ArgumentNullException("Name not given");
            else if (_songRepo.GetByName(song.Name) != null) throw new ArgumentException("Person with Name '" + song.Name + "' already exists!");
            else
            {
                findArtist.Name = song.Name ?? findArtist.Name;
                _songRepo.Update(findArtist);
                _songRepo.SaveChanges();
            }
        }

        public void AddSongs(IEnumerable<SongDto> songs)
        {
            _songRepo.AddRange(ConvertAndFilter(songs));
            _songRepo.SaveChanges();
        }

        private IEnumerable<Song> ConvertAndFilter(IEnumerable<SongDto> songDtos)
        {
            // First get all the current 
            List<Song> currentSongs = GetAll();

            // Convert the new SongDto's to Songs
            List<Song> newSongs = new List<Song>();
            foreach (SongDto songDto in songDtos)
            {
                newSongs.Add(ConvertDto(songDto));
            }

            // Find incomplete Songs
            List<Song> incompleteSongs = newSongs.Where(s => s.Name == "" || s.Shortname == "" || s.SpotifyId == null || s.Artist == null).ToList();
            // if (incompleteSongs.Any()) throw new ArgumentException("Provided list of new Songs contains at least one incomplete Song, namely the one with Id " + incompleteSongs.First().Id);
            newSongs = newSongs.Where(s => s.Genre == "Metal").ToList();

            // Now filter: keep the new songs - minus the existing Songs - minus the incomplete songs
            return newSongs.Except(currentSongs).Except(incompleteSongs);

        }

        private Song ConvertDto(SongDto songDto)
        {
            return new Song
            {
                Duration = songDto.Duration,
                Album = songDto.Album,
                Artist = _artistRepo.GetByName(songDto.Artist),
                BPM = songDto.BPM ?? 0,
                Genre = songDto.Genre,
                Id = songDto.Id,
                Name = songDto.Name,
                Shortname = songDto.Shortname,
                SpotifyId = songDto.SpotifyId ?? string.Empty,
                Year = songDto.Year
            };
        }

        public void Add(SongDto song)
        {
            if (song.Name == "") throw new ArgumentNullException("Artist Name empty for id " + song.Id.ToString());
            else if (Exists(song)) throw new ArgumentException("Artist with Name " + song.Name + " and Id " + song.Id.ToString() + " already exists");
            else
            {
                _songRepo.Add(ConvertDto(song)); 
                _songRepo.SaveChanges();
            }
        }
    }
}
