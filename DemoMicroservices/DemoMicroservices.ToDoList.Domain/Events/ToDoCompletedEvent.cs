namespace DemoMicroservices.ToDoList.Domain.Events
{
    public class ToDoCompletedEvent : Event
    {
        public int Id { get; }

        public string Name { get; }

        public ToDoCompletedEvent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
