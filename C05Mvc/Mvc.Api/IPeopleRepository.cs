using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Api
{
    public interface IPeopleRepository
    {
        IEnumerable<string> All { get; }
    }
}
