using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace VDMJasminka.Core.Exceptions
{
    public class PropertyMappingException : Exception
    {
        public PropertyMappingException(MemberInfo property) : base($"The property {property.Name} has no description")
        {

        }
        public PropertyMappingException(string message) : base(message)
        {

        }
    }
}
