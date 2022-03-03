using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ChefActionInfoAttribute : Attribute
    {
        public string Name { get; private set; }
        public ChefActionInfoAttribute(string friendlyName)
        {
            Name = friendlyName;           
        }
    }
}
