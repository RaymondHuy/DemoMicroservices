namespace DemoMicroservices.ToDoList.Domain.Events
{
    public class ToDoCompletedEvent : Event
    {
        public int Id { get; }

        public ToDoCompletedEvent(int id)
        {
            Id = id;
        }
    }
}
