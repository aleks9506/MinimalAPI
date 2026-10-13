using ToDo.Application.Common.Dto;

namespace ToDo.Application.Services
{
    public interface IToDoTaskService
    {
        Task Create(CreateToDoTaskDto createToDoTask);
    }
}
