using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeService
{
    // this impl isn't used, but it's confusing if it isn't here (i.e. only the test impl exists)
    public class PersonRepository : IPersonRepository
    {
        public string[] GetNames()
        {
            return new string[] { "Fred", "Wilma" };
        }
    }
}
