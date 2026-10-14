using ToDo.Application.Common.Dto;
using ToDo.Infrasctructure.Data.Entities;
using ToDo.Application.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Infrasctructure.Data.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly ToDoDbContext _dbContext;

        public ToDoTaskRepository(ToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetToDoTaskDto?> Get(int id)
        {
            return await _dbContext.ToDoTasks
                .Where(x => x.Id == id &&
                !x.Deleted.HasValue)
                .Select(x => new GetToDoTaskDto
                {
                    Id = x.Id,
                    Task = x.Task,
                    Created = x.Created
                })
                .SingleOrDefaultAsync();
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

        public async Task<bool> Update(int id, UpdateToDoTaskDto updateToDoTask)
        {
            var toDoTask = await _dbContext.ToDoTasks
                .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted.HasValue);

            if (toDoTask == null) return false;

            toDoTask.Task = updateToDoTask.Task;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var toDoTask = await _dbContext.ToDoTasks
                .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted.HasValue);

            if (toDoTask == null) return false;

            toDoTask.Deleted = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
