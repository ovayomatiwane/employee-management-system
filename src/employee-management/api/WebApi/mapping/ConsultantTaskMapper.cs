using WebApi.Dtos;
using WebApi.Models.Entities;

namespace WebApi.mapping
{
    public static class ConsultantTaskMapper
    {
        public static ConsultantTask MapToEntity(this ConsultantTaskDto consultantTaskDto)
        {
            return new()
            {
                Id = consultantTaskDto.Id,
                RoleRateId = consultantTaskDto.RoleId,
                TaskId = consultantTaskDto.TaskId,
                ConsultantId = consultantTaskDto.ConsultantId,
                AssignedDate = consultantTaskDto.AssignedDate,
                HoursCompleted = consultantTaskDto.HoursCompleted,
                AssignedHours = consultantTaskDto.AssignedHours,
                Consultant = consultantTaskDto.Consultant.MapToEntity(),
                Task = consultantTaskDto.Task.MapToEntity(),
                RoleRate = consultantTaskDto.RoleRate.MapToEntity(),
            };
        }

        public static IEnumerable<ConsultantTask> MapToEntities(this IEnumerable<ConsultantTaskDto> consultantTaskDtos)
        {
            return consultantTaskDtos.Select(x => x.MapToEntity());
        }

        public static ConsultantTaskDto MapToDto(this ConsultantTask consultantTask)
        {
            return new()
            {
                Id = consultantTask.Id,
                RoleId = consultantTask.RoleRateId,
                TaskId = consultantTask.TaskId,
                ConsultantId = consultantTask.ConsultantId,
                AssignedDate = consultantTask.AssignedDate,
                HoursCompleted = consultantTask.HoursCompleted,
                AssignedHours = consultantTask.AssignedHours,
                Consultant = consultantTask.Consultant.MapToDto(),
                Task = consultantTask.Task.MapToDto(),
                RoleRate = consultantTask.RoleRate.MapToDto(),
            };
        }

        public static IEnumerable<ConsultantTaskDto> MapToDtos(this IEnumerable<ConsultantTask> consultantTasks)
        {
            return consultantTasks.Select(x => x.MapToDto());
        }
    }
}
