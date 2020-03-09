using DemoMicroservices.ToDoList.SearchApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMicroservices.ToDoList.SearchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchDbContext _dbContext;

        public SearchController(SearchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<SearchResultViewModel>> Get([FromQuery]string keyword)
        {
            return await _dbContext.Tags
                .Where(n => n.Keyword.StartsWith(keyword))
                .Select(n => new SearchResultViewModel()
                {
                    Value = n.Value,
                    ReferenceId = n.ReferenceId
                })
                .ToListAsync();
        }
    }

    public class SearchResultViewModel
    {
        public string Value { get; set; }

        public int ReferenceId { get; set; }
    }
}
