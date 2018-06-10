using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.Api.Models
{
    public interface IPeopleRepository
    {
        Task<PersonDto> GetOneAsync(int id);
    }
}