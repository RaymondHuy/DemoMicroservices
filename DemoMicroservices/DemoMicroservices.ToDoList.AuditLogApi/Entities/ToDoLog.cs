using System;

namespace DemoMicroservices.ToDoList.AuditLogApi.Entities
{
    public class ToDoLog
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
