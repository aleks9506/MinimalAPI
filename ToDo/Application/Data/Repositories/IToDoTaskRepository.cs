using ToDo.Application.Common.Dto;

namespace ToDo.Application.Data.Repositories
{
    public interface IToDoTaskRepository
    {
        Task Create(CreateToDoTaskDto createToDoTask);
    }
}
