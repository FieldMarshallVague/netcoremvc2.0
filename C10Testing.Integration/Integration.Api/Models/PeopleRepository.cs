using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Integration.Api.Models
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly HttpClient client;

        public PeopleRepository(HttpClient httpClient)
        {
            this.client = httpClient;
        }

        public async Task<PersonDto> GetOneAsync(int id)
        {
            var personResponse = await client.GetAsync($"http://localhost:50846/api/values/{id}");
            var personName = await personResponse.Content.ReadAsStringAsync();

            return new PersonDto
            {
                Id = id,
                Name = personName
            };
        }
    }
}