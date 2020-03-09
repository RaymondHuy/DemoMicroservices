namespace DemoMicroservices.ToDoList.SearchApi.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Keyword { get; set; }

        public string Value { get; set; }

        public int ReferenceId { get; set; }
    }
}
