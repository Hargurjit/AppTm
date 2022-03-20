using AppTm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Repositories
{
    public class ArtistRepository : IRepository<Artist>
    {
        private readonly Context context;

        public ArtistRepository(Context context)
        {
            this.context = context;
        }

        public Artist Get(int id)
        {
            return context.Set<Artist>().Find(id);
        }

        public List<Artist> GetAll()
        {
            return context.Set<Artist>().ToList();
        }
    }
}
