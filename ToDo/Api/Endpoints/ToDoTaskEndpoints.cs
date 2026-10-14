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

            group.MapGet("{id:int}", Get)
                .Produces<GetToDoTaskDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return app;
        }

        public static async Task<Results<Ok<GetToDoTaskDto>, NotFound>> Get
            (int id, 
            IToDoTaskService toDoTaskService)
        {
            var toDoTask = await toDoTaskService.Get(id);
            if (toDoTask == null) return TypedResults.NotFound();

            return TypedResults.Ok(toDoTask);
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
