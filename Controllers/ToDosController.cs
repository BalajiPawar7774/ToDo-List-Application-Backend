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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = _mapper.Map<Todo>(dto);
            var createdTodo = await _commonRepository.AddAsync(todo);
            if(createdTodo == null)
            {
                return BadRequest(new { status = false, message = "Something Went wrong. Could not create todo"});
            }
            return Ok(new { status = true, message = "Todo created successfully", data = createdTodo } );
        }

        // create Delete action Method here
    }
}
