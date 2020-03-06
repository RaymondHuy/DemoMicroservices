using System;

namespace DemoMicroservices.ToDoList.Domain.Events
{
    public abstract class Event
    {
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}
