using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.DomainModels;

namespace TicketsApp.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T Get(Guid? id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}
