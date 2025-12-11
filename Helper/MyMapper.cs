using AutoMapper;
using ToDoApplication.Dto;
using ToDoApplication.Model;

namespace ToDoApplication.Helper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<TodoDto, Todo>();
            CreateMap<Todo, TodoDto>();



        }
    }
}
