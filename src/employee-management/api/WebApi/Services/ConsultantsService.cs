using Microsoft.EntityFrameworkCore;
using WebApi.CustomExceptions;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.mapping;
using WebApi.Models.Entities;
using WebApi.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WebApi.Services
{
    public class ConsultantsService(EmployeeManagementDataContext databaseContext) : IConsultantsService
    {
        public async Task CreateConsultantAsync(ConsultantDto consultant, CancellationToken cancellationToken = default)
        {
            var consultantEntity = new Consultant()
            {
                Id = Guid.NewGuid(),
                FirstName = consultant.FirstName,
                LastName = consultant.LastName,
            };

            databaseContext.Consultants.Add(consultantEntity);

            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ConsultantDto>> GetAllConsultantsAsync(CancellationToken cancellationToken = default)
        {
            var consultants =  await databaseContext.Consultants.ToListAsync(cancellationToken);

            return consultants.MapToDtos();
        }

        public async Task<ConsultantDto> GetConsultantAsync(Guid consultantId, CancellationToken cancellationToken = default)
        {
            var consultant = await databaseContext.Consultants
                                                  .FirstOrDefaultAsync(x => x.Id == consultantId, cancellationToken);

            if (consultant is null)
            {
                string message = $"Consultant with Id: {consultantId} not found.";
                throw new EntityNotFoundException(message);
            }
            
            return consultant.MapToDto();
        }

        public async Task RemoveConsultantAsync(Guid consultantId, CancellationToken cancellationToken = default)
        {
            var consultant = await databaseContext.Consultants
                                                  .FirstOrDefaultAsync(x => x.Id == consultantId, cancellationToken);

            if (consultant is null)
            {
                string message = $"Consultant with Id: {consultantId} not found. Please remove an existing consultant.";
                throw new EntityNotFoundException(message);
            }

            databaseContext.Consultants.Remove(consultant);
            await databaseContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateConsultantAsync(ConsultantDto consultant, CancellationToken cancellationToken = default)
        {
            var consultantEntity = await databaseContext.Consultants
                                                  .FirstOrDefaultAsync(x => x.Id == consultant.Id, cancellationToken);

            if (consultantEntity is null)
            {
                string message = $"Consultant with Id: {consultant.Id} not found. Please update an existing consultant.";
                throw new EntityNotFoundException(message);
            }
        }
    }
}
