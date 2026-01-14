using WebApi.Dtos.QueryData;
using WebApi.Dtos.ResponseData;

namespace WebApi.Services.Interfaces
{
    public interface IBusinessRulesService
    {
        Task<TotalOwedDto> GetTotalOwedAsync(TimeFrameDto timeFrame, CancellationToken cancellation = default);
    }
}
