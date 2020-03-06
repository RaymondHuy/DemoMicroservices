namespace DemoMicroservices.ToDoList.CommandApi.Entities
{
    public class ToDo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }
    }
}
