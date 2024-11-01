using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using System.Linq;

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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem todo)
        {
            _context.ToDoItems.Add(todo);
            _context.SaveChanges();
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ToDoItem todo)
        {
            var existingToDo = _context.ToDoItems.Find(id);
            if (existingToDo == null)
            {
                return NotFound();
            }

            existingToDo.JobToDo = todo.JobToDo;
            existingToDo.Status = todo.Status;
            existingToDo.StartDate = todo.StartDate;

            _context.SaveChanges();
            return Ok(existingToDo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(todo);
            _context.SaveChanges();
            return Ok();
        }
    }
}
