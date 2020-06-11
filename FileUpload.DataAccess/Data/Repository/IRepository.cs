using FileUpload.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.DataAccess.Data.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();
        Task<FileDescription> GetFileDescription(int id);
        Task<User> GetUser(int userId);
    }
}
