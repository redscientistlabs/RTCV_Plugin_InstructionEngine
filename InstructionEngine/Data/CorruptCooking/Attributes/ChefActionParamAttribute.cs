using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructionEngine.Data.CorruptCooking
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    sealed class ChefActionParamAttribute : Attribute
    {
        public int Position { get; private set; }
        public string Name { get; private set; }
        public int LinkID { get; private set; }

        public Type ParamType { get; private set; }

        public ChefActionParamAttribute(int position, string name, Type paramType, int linkID = -1)
        {
            Position = position;
            Name = name;
            ParamType = paramType;
            LinkID = linkID;
        }

    }
}
