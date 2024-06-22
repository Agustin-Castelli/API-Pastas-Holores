using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BaseRepository<T> where T : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        // static int LastIdAssigned = 0;

        public T? GetById<TId>(TId id)
        {
            return _context.Set<T>().Find(new object[] { id } );
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            // client.Id = LastIdAssigned++;
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
