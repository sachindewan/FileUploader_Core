using FileUpload.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload.DataAccess.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _applicationDbContext.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _applicationDbContext.Remove<T>(entity);
        }

        public Task<FileDescription> GetFileDescription(int id)
        {
            return _applicationDbContext.FileDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(d => d.Id == userId);
            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }
    }
}
