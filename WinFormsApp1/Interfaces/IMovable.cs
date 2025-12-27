using System;
using System.Collections.Generic;
using System.Linq;
using EZInput;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Interfaces
{

    public interface IMovable
    {
        // Velocity of the object
        PointF Velocity { get; set; }
    }
}
