using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCore.Helpers;
using WebApiCore.Models;

namespace WebApiCore.Data.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entity) where T : class;
        public void AddFileRange<T>(IEnumerable<T> entity, long fileSize) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();
        Task<FileDescription> GetFileDescription(int id);
        Task<IEnumerable<FileDescription>> GetAllUserFileDescription(int userId);
        Task<User> GetUser(int userId);

        Task<IEnumerable<ImportFileDescription>> GetAllUserImportedFileDescription(int userId);
        Task<ImportFileDescription> GetImportedFileDescription(int id);
    }
}
