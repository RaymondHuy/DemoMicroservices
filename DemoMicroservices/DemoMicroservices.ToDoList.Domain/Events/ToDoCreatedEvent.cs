using System;
using System.Collections.Generic;
using System.Text;

namespace DemoMicroservices.ToDoList.Domain.Events
{
    public class ToDoCreatedEvent : Event
    {
        public int Id { get; }

        public string Name { get; }

        public ToDoCreatedEvent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
