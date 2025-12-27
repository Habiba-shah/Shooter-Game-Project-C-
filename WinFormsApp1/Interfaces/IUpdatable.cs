using GameProjectOop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using EZInput;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Interfaces
{

    public interface IUpdatable
    {
        // Method to update the object
        void Update(GameTime gameTime);
    }
}
