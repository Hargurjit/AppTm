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
    public class ArtistsController : ControllerBase
    {

        private readonly IArtistService _artistService;
        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IEnumerable<Artist> GetArtists()
        {
            return _artistService.GetAll();
        }

        [HttpPut("{id}")]
        public void UpdateArtist(int id, [FromBody] Artist artist)
        {
            _artistService.Update(id, artist);
        }

        [HttpDelete("{id}")]
        public void DeleteArtist(int id)
        {
            _artistService.Delete(id);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Artist>> AddArtists(Artist[] artists)
        {
            try
            {
                _artistService.AddArtists(artists);
                return Created("",artists);
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
