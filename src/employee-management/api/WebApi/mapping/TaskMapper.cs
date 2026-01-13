using WebApi.Dtos;
using Task = WebApi.Models.Entities.Task;

namespace WebApi.mapping
{
    public static class TaskMapper
    {
        public static Task MapToEntity(this TaskDto taskDto)
        {
            return new()
            {
                Id = taskDto.Id,
                TaskName = taskDto.TaskName,
                Description = taskDto.Description,
                MaxDuration = taskDto.MaxDuration,
                CreatedDate = taskDto.CreatedDate,
                CompletionDate = taskDto.CompletionDate
            };
        }

        public static IEnumerable<Task> MapToEntities(this IEnumerable<TaskDto> taskDtos)
        {
            return taskDtos.Select(x => x.MapToEntity());
        }

        public static TaskDto MapToDto(this Task task)
        {
            return new()
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Description = task.Description,
                MaxDuration = task.MaxDuration,
                CreatedDate = task.CreatedDate,
                CompletionDate = task.CompletionDate
            };
        }

        public static IEnumerable<TaskDto> MapToDtos(this IEnumerable<Task> tasks)
        {
            return tasks.Select(x => x.MapToDto());
        }
    }
}
