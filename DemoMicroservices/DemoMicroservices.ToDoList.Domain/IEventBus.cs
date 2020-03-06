using DemoMicroservices.ToDoList.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.Domain
{
    public interface IEventBus
    {
        void Publish(Event @event);
    }
}
