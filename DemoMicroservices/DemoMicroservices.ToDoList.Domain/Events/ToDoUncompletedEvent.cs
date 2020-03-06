namespace DemoMicroservices.ToDoList.Domain.Events
{
    public class ToDoUncompletedEvent : Event
    {
        public int Id { get; set; }

        public ToDoUncompletedEvent(int id)
        {
            Id = id;
        }
    }
}
