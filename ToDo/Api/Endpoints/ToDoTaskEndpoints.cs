using Microsoft.AspNetCore.Http.HttpResults;
using ToDo.Application.Common.Dto;
using ToDo.Application.Services;

namespace ToDo.Api.Endpoints
{
    public static class ToDoTaskEndpoints
    {
        public static WebApplication MapToDoTaskEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/todo-tasks");

            group.MapPost("/", Create)
                .ProducesValidationProblem()
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return app;
        }

        public static async Task<Created> Create(
            CreateToDoTaskDto createToDoTask, 
            IToDoTaskService toDoTaskService
            )
        { 
            await toDoTaskService.Create(createToDoTask);

            return TypedResults.Created();
        }
    }
}
