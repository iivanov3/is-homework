using System;
using System.Collections.Generic;
using System.Text;
using TicketsApp.Domain.Identity;

namespace TicketsApp.Repository.Interface
{
    public interface IUserRepository
    {
        List<AppUser> GetAll();
        AppUser Get(string id);
        void Insert(AppUser entity);
        void Update(AppUser entity);
        void Delete(AppUser entity);
        void Remove(AppUser entity);
        void SaveChanges();
    }
}
