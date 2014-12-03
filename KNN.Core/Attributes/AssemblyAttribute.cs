using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Core.Attributes
{
        [AttributeUsage(AttributeTargets.All)]
    public class AssemblyAttribute : Attribute
    {
                        // Private fields. 
    private string assemblyName;
            private string className;
            private string methodName;
            public AssemblyAttribute(string asemblyName, string className, string methodName)
    {
        this.assemblyName = asemblyName;
        this.className = className;
        this.methodName = methodName;

    }
            public virtual string AssemblyName
    {
        get { return assemblyName; }
        set { assemblyName = value; }
    }
            public virtual string ClassName
            {
                get { return className; }
                set { className = value; }
            }
            public virtual string MethodName
            {
                get { return methodName; }
                set { methodName = value; }
            }
    }
}
