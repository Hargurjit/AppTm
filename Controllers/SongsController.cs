using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppTm.Entities;
using AppTm.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppTm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {

        private readonly ISongService _songService;
        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public IEnumerable<Song> GetSongs()
        {
            return _songService.GetAll();
        }

        [HttpPut("{id}")]
        public void UpdateSong(int id, [FromBody] SongDto song)
        {
            _songService.Update(id, song);
        }

        [HttpDelete("{id}")]
        public void DeleteSong(int id)
        {
            _songService.Delete(id);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Artist>> AddSongs(SongDto[] songs)
        {
            try
            {
                _songService.AddSongs(songs);
                return Created("", songs);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
