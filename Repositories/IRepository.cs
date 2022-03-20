using AppTm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);
        List<T> GetAll();
    }
}
