using Dto.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAll();
        Task<CustomerDto> Get(int id);

        Task Create(CustomerDto customerDto);

        Task Update(CustomerDto customerDto);

        Task Delete(int id);
    }
}
