using EZInput;
using GameProjectOop.Core;
using GameProjectOop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Interfaces
{

    public interface IMovement
    {
        void Move(GameObject obj, GameTime gameTime);
    }
}
