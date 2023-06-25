using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginComponent.Services
{
    public class SomeComponentService : ISomeComponentService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
