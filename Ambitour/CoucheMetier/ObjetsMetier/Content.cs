using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    [XmlInclude(typeof(Handle))]
    //Add other subclasses if necessary
    public abstract class Content
    {
    }
}
