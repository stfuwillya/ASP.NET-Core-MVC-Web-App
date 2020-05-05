using InfraLayer.Context;
using InfraLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfraLayer.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _entity;
        private readonly EmpDepContext _empDepContext;
        public Repository(EmpDepContext empDepContext) 
        {
            _empDepContext = empDepContext;
            _entity = _empDepContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _empDepContext.Add<TEntity>(entity);
            _empDepContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var dataToDelete = GetById(id);
            _entity.Remove(dataToDelete);
            _empDepContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entity.ToList<TEntity>(); 
        }

        public TEntity GetById(int id)
        {
            return _entity.Find(id);
        }

        public void Update(TEntity entity)
        {
            _empDepContext.Update<TEntity>(entity);
            _empDepContext.SaveChanges();
        }
    }
}
