using DemoMicroservices.ToDoList.CommandApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.CommandApi.Repositories
{
    public interface IToDoRepository
    {
        Task CreateAsync(ToDo entity);
        Task<IEnumerable<ToDo>> GetAll();
        Task UpdateAsync(ToDo entity);
        Task<ToDo> GetByIdAsync(int id);
    }
}
