using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ConsultantTasksService : IConsultantTasksService
    {
        public async Task CreateConsultantTaskAsync(ConsultantTaskDto consultantTask, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ConsultantTaskDto>> GetAllConsultantTasksAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ConsultantTaskDto> GetConsultantTaskAsync(Guid consultantTaskId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveConsultantTaskAsync(Guid consultantTaskId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateConsultantTaskAsync(ConsultantTaskDto consultantTask, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
