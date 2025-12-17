using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Dto;
using ToDoApplication.Model;
using ToDoApplication.Repositories;

namespace ToDoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ICommonRepository<Todo> _commonRepository;
        private readonly IMapper _mapper;
        public ToDosController(ICommonRepository<Todo> commonRepository, IMapper _mapper)
        {
            _commonRepository = commonRepository;
            this._mapper = _mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodo([FromBody] TodoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = _mapper.Map<Todo>(dto);
            var createdTodo = await _commonRepository.AddAsync(todo);
            if (createdTodo == null)
            {
                return BadRequest(new { status = false, message = "Something Went wrong. Could not create todo" });
            }
            return Ok(new { status = true, message = "Todo created successfully", data = createdTodo });
        }

        // create Delete action Method here
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var result = await _commonRepository.DeleteAsync(id);
            if (result)
            {
                return Ok(new { status = true, message = "Todo deleted successfully" });
            }
            return BadRequest(new { status = false, message = $"Todo not found with id {id}" });
        }

        // create getById Action method here
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _commonRepository.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new { status = false, message = $"Todo with id {id} not found " });
            }
            return Ok(new { status = true, data = todo });
        }

        // create getAll here
        [HttpGet("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _commonRepository.GetAllAsync();
            if (todos == null || todos.Count == 0)
            {
                return NotFound(new { status = false, message = "No todoes available"});
            }
            return Ok(new { status = true, data = todos });
        }

        // Add check point with some action method
    }
}
