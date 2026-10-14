using ToDo.Application.Common.Dto;

namespace ToDo.Application.Data.Repositories
{
    public interface IToDoTaskRepository
    {
        Task<GetToDoTaskDto?> Get(int id);
        Task Create(CreateToDoTaskDto createToDoTask);
    }
}
