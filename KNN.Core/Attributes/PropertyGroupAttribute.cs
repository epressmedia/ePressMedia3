using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPM.Core.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class PropertyGroupAttribute: Attribute
    {
            // Private fields. 
    private string name;
    public PropertyGroupAttribute(string name)
    {
        this.name = name;

    }
            public virtual string Name
    {
        get { return name; }
        set { name = value; }
    }


    }
}
