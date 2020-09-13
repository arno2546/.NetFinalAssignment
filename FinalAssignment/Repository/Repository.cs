using FinalAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace testFinalAssignment.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AssignmentDataContext context = new AssignmentDataContext();
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            context.Set<T>().Remove(GetById(id));
            context.SaveChanges();
        }

        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }                                                   

      
    }
}