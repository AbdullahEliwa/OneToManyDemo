using Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class DbService
    {
        #region CTOR, Fields.
        private readonly ApplicationDbContext _context;

        public DbService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Utilities.

        public async Task Do(Func<Task> func)
        {
            await func();
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new ArgumentException("Failed to save changes!");
        }

        public async Task Do(Action action)
        {
            action();
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new ArgumentException("Failed to save changes!");
        }
        #endregion

        public async Task Insert<T>(T obj)
        {
            await Do(async () => await _context.AddAsync(obj));
        }

        public async Task Update<T>(T obj)
        {
            await Do(() => _context.Entry(obj).State = EntityState.Modified);
        }

        public async Task Delete<T>(T obj)
        {
            await Do(() => _context.Remove(obj));
        }
    }
}
