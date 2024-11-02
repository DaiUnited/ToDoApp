using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _repository;

        public ToDoController(IConfiguration configuration)
        {
            _repository = new ToDoRepository(configuration);
        }

        [HttpGet]
        public ActionResult<List<ToDoItem>> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem todo)
        {
            _repository.Add(todo);
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ToDoItem todo)
        {
            todo.Id = id;
            _repository.Update(todo);
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }

        [HttpPut("UpdateStatus/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] ToDoItem todo)
        {
            _repository.UpdateStatus(todo);
            return Ok(todo);
        }

    }
}
