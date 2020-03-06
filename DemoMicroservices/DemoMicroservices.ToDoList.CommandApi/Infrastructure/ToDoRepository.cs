using DemoMicroservices.ToDoList.CommandApi.Entities;
using DemoMicroservices.ToDoList.CommandApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace DemoMicroservices.ToDoList.CommandApi.Infrastructure
{
    internal class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(ToDo entity)
        {
            _dbContext.ToDos.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDo>> GetAll()
        {
            return await _dbContext.ToDos.OrderByDescending(n => n.Id).ToListAsync();
        }

        public async Task<ToDo> GetByIdAsync(int id)
        {
            return await _dbContext.ToDos.AsNoTracking().SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateAsync(ToDo entity)
        {
            _dbContext.ToDos.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
