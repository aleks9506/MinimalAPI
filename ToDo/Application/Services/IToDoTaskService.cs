using ToDo.Application.Common.Dto;

namespace ToDo.Application.Services
{
    public interface IToDoTaskService
    {
        Task<GetToDoTaskDto?> Get(int id);

        Task Create(CreateToDoTaskDto createToDoTask);

        Task<bool> Update(int id, UpdateToDoTaskDto updateToDoTask);
    }
}
