using AppTm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTm.Repositories
{
    public interface IRepository<T>
    {
        public T GetById(int id);
        public T GetByName(string name);
        public List<T> GetAll();
        public void Update(T entity);
        public void Add(T entity);
        public void Delete(int id);
        public void AddRange(IEnumerable<T> entity);
        public void SaveChanges();
    }
}
