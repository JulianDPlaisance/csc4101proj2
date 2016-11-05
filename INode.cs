using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    public interface INode
    {
        INode GetCar();
        INode GetCdr();
    }
}
