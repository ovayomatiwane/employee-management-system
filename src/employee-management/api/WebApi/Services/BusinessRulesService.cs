using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos.QueryData;
using WebApi.Dtos.ResponseData;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class BusinessRulesService(
        EmployeeManagementDataContext databaseContext) : IBusinessRulesService
    {
        public async Task<TotalOwedDto> GetTotalOwedAsync(TimeFrameDto timeFrame, CancellationToken cancellation = default)
        {
            var consultantTasks = await databaseContext.ConsultantTasks
                                                       .Include(x => x.Consultant)
                                                       .Include(x => x.RoleRate)
                                                       .Where(x => x.ConsultantId == timeFrame.ConsultantId
                                                                   && x.RoleRate.StartDate >= timeFrame.TimeFrom 
                                                                   && (x.RoleRate.EndDate <= timeFrame.TimeTo || x.RoleRate.EndDate == null))
                                                       .ToListAsync(cancellation);


            if (consultantTasks.Count == 0)
            {
                return new()
                {
                    TotalOwed = 0,
                    TimeTo = timeFrame.TimeTo,
                    TimeFrom = timeFrame.TimeFrom,
                    ConsultantId = timeFrame.ConsultantId,
                    FullNames = string.Empty
                };
            }

            float totalAllOwed = 0;

            foreach(var consultantTask in consultantTasks)
            {
                totalAllOwed += consultantTask.HoursCompleted * consultantTask.RoleRate.HourlyRate;
            }

            var consultant = consultantTasks.FirstOrDefault()?.Consultant;

            return new()
            {
                TotalOwed = totalAllOwed,
                TimeTo = timeFrame.TimeTo,
                TimeFrom = timeFrame.TimeFrom,
                ConsultantId = timeFrame.ConsultantId,
                FullNames = consultant is not null? $"{consultant.FirstName} {consultant.LastName}" : string.Empty,
            };
        }
    }
}
