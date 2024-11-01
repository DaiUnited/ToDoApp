using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var todos = _context.ToDoItems.ToList();
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem todo)
        {
            _context.ToDoItems.Add(todo);
            _context.SaveChanges();
            return Ok(todo);
        }
    }
}
