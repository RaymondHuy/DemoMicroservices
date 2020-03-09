namespace DemoMicroservices.ToDoList.Domain.Events
{
    public class ToDoUncompletedEvent : Event
    {
        public int Id { get; }

        public string Name { get; }

        public ToDoUncompletedEvent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
