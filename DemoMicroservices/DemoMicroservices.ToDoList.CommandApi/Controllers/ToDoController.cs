using DemoMicroservices.ToDoList.CommandApi.Entities;
using DemoMicroservices.ToDoList.CommandApi.Repositories;
using DemoMicroservices.ToDoList.Domain;
using DemoMicroservices.ToDoList.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.CommandApi.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IEventBus _eventBus;

        public ToDoController(
            IToDoRepository toDoRepository,
            IEventBus eventBus)
        {
            _toDoRepository = toDoRepository;
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task Create([FromBody]ToDo toDo)
        {
            toDo.IsDone = false;
            await _toDoRepository.CreateAsync(toDo);

            _eventBus.Publish(new ToDoCreatedEvent(toDo.Id, toDo.Name));
        }

        [HttpGet]
        public async Task<IEnumerable<ToDo>> Get()
        {
            return await _toDoRepository.GetAll();
        }

        [HttpPut("complete")]
        public async Task Complete([FromRoute]int id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);

            todo.IsDone = true;

            await _toDoRepository.UpdateAsync(todo);
        }

        [HttpPut("uncomplete")]
        public async Task UnComplete([FromRoute]int id)
        {
            var todo = await _toDoRepository.GetByIdAsync(id);

            todo.IsDone = false;

            await _toDoRepository.UpdateAsync(todo);
        }
    }
}
