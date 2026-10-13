using ToDo.Application.Common.Dto;
using ToDo.Application.Data.Repositories;

namespace ToDo.Application.Services.Implementations
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IToDoTaskRepository _toDoTaskRepository;
        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository)
        {
            _toDoTaskRepository = toDoTaskRepository;
        }
        public async Task Create(CreateToDoTaskDto createToDoTask)
        {
            await _toDoTaskRepository.Create(createToDoTask);
        }
    }
}
