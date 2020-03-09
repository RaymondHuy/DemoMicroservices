using DemoMicroservices.ToDoList.AuditLogApi.Entities;
using DemoMicroservices.ToDoList.AuditLogApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.AuditLogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly AuditLogDbContext _dbContext;

        public AuditLogController(AuditLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<ToDoLog>> Get()
        {
            return await _dbContext.ToDoLogs.ToListAsync();
        }
    }
}
