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
    public class UsersController : ControllerBase
    {
        private readonly ICommonRepository<User> _commonRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(ICommonRepository<User> commonRepository, IMapper mapper, UserRepository userRepository)
        {
            _commonRepository = commonRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            if (_userRepository.IsEmailExistsAsync(dto.Email).Result)
            {
                return BadRequest(new { status = false, message = $"User with {dto.Email} already Exists!!" });
            }
            var user = _mapper.Map<User>(dto);
            var addedUser = await _commonRepository.AddAsync(user);
            if (addedUser != null)
            {
                return Ok(new { status = true, message = "User Registered Successfully", data = addedUser });
            }
            else
            {
                return BadRequest(new { status = false, message = "Something went wrong!!" });
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var user = _userRepository.IsValidUserAsync(dto.Email, dto.Password).Result;
            if (user != null)
            {
                return Ok(new { status = true, message = "Login Successful", data = user });
            }
            else
            {
                return BadRequest(new { status = false, message = "Invalid Email or Password" });
            }
        }

        [HttpGet("getTodos/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodos(int userId)
        {
            var todoes = await _userRepository.GetListOfTodoes(userId);
            if (todoes == null || todoes.Count == 0)
            {
                return BadRequest(new { status = false, message = "No Todoes found for the user" });
            }
            return Ok(new { status = true, message = "Todoes fetched successfully", data = todoes });
        }

        [HttpPut("updateStatus/{todoId}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(int todoId, int status)
        {
            var isUpdated = await _userRepository.UpdateStatus(todoId, status);
            if (!isUpdated)
            {
                return BadRequest(new { status = false, message = "Could not update status. Todo not found." });
            }
            return Ok(new { status = true, message = "Status updated successfully" });
        }

        [HttpGet("updatePriority/{todoId}/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePriority(int todoId, int priority)
        {
            var isUpdated = await _userRepository.UpdatePriority(todoId, priority);
            if (!isUpdated)
            {
                return BadRequest(new { status = false, message = "Could not update priority. Todo not found." });
            }
            return Ok(new { status = true, message = "Priority updated successfully" });
        }


        [HttpGet("getnotCompletedTodos/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotCompletedTodos(int userId)
        {
            var notCompletedTodos = await _userRepository.GetNotCompletedOnly(userId);
            if (notCompletedTodos == null || notCompletedTodos.Count == 0)
            {
                return BadRequest(new { status = false, message = "No not completed Todoes found for the user" });
            }
            return Ok(new { status = true, message = "Not completed Todoes fetched successfully", data = notCompletedTodos });
        }

        [HttpGet("getaccordingtopriority/userid/{userId}/priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccordingToPriority(int userId, int priority)
        {
            var todoes = _userRepository.GetAccrdingtoPriority(userId, priority);

            var priorityName = "";

            if (priority == 0) priorityName = "Low";
            else if (priority == 1) priorityName = "Medium";
            else priorityName = "High";

            if (todoes.Result == null || todoes == null || todoes.Result.Count == 0)
            {
                return BadRequest(new { status = false, message = $"No Todoes {priorityName} priority found" });
            }
            return Ok(new { status = true, message = $" Todoes of {priorityName} priority fetched successfully", data = todoes.Result });
        }


        [HttpGet("getaccordingtostatus/userid/{userId}/status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccordingToStatus(int userId, int status)
        {
            var todoes = _userRepository.GetAccrodingToStatus(userId, status);

            var statusName = "";

            if (status == 0) statusName = "Created";
            else if (status == 1) statusName = "Working";
            else statusName = "Completed";

            if (todoes.Result == null || todoes == null || todoes.Result.Count == 0)
            {
                return BadRequest(new { status = false, message = $"No todoes of  {statusName} status found" });
            }
            return Ok(new { status = true, message = $"Todoes of {statusName} status fetched successfully", data = todoes.Result });
        }


        // create a password change action method here 



    }
}