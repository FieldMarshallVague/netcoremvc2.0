using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeSauce.Api
{
    public interface IComponent
    {
        string Name { get; }
    }

    public class ComponentA : IComponent
    {
        private readonly IComponent _componentB;
        public ComponentA (IComponent componentB)
        {
            _componentB = componentB;
        }

        public string Name { get; set; } = nameof(ComponentB);
    }

    public class ComponentB : IComponent {
        public string Name { get; set; } = nameof(ComponentB);
    }
}
