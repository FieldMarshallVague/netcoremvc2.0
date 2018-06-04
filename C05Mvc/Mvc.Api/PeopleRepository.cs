using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Api
{
    public class PeopleRepository : IPeopleRepository
    {
        public IEnumerable<string> All => new string[] { "Fanie", "John", "Joe" };
    }
}
