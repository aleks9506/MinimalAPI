using ToDo.Application.Common.Dto;
using ToDo.Infrasctructure.Data.Entities;
using ToDo.Application.Data.Repositories;

namespace ToDo.Infrasctructure.Data.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoTaskRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Create(CreateToDoTaskDto createToDoTask)
        {
            await _dbContext.ToDoTasks.AddAsync(new ToDoTask
            {
                Task = createToDoTask.Task,
                Created = DateTime.UtcNow
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
