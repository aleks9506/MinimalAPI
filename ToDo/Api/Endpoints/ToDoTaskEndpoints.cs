using Microsoft.AspNetCore.Http.HttpResults;
using ToDo.Application.Common.Dto;
using ToDo.Application.Services;

namespace ToDo.Api.Endpoints
{
    public static class ToDoTaskEndpoints
    {
        public static WebApplication MapToDoTaskEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/to-do-tasks")
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapPost("/", Create)
                .ProducesValidationProblem();

            group.MapGet("{id:int}", Get);

            group.MapGet("listing/{p:int:min(1)}", GetListing);

            group.MapPut("{id:int}", Update)
                .ProducesValidationProblem();

            group.MapDelete("{id:int}", Delete);

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

        public static async Task<Results<Ok<ToDoTaskListingResultDto>, NotFound>> GetListing(
            [AsParameters] ToDoTaskListingDto listing,
            IToDoTaskService toDoTaskService
            )
        {
            var result = await toDoTaskService.GetListing(listing);

            if(listing.Page > 1 || listing.Page > result.TotalPages)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(result);
        }

        public static async Task<Created> Create(
            CreateToDoTaskDto createToDoTask, 
            IToDoTaskService toDoTaskService
            )
        { 
            await toDoTaskService.Create(createToDoTask);

            return TypedResults.Created();
        }

        public static async Task<Results<NoContent, NotFound>> Update(
            int id,
            UpdateToDoTaskDto updateToDoTask,
            IToDoTaskService toDoTaskService
            )
        {
            if (!await toDoTaskService.Update(id, updateToDoTask)) return TypedResults.NotFound();
            return TypedResults.NoContent();
        }

        public static async Task<Results<NoContent, NotFound>> Delete(
            int id,
            IToDoTaskService toDoTaskService
            )
        {
            if (!await toDoTaskService.Delete(id)) return TypedResults.NotFound();
            return TypedResults.NoContent();
        }

    }
}
