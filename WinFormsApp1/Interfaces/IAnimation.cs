using GameProjectOop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Interfaces
{
    public interface IAnimation
    {
        void Update(GameTime gameTime);
        void Play(string animationState);
        Image GetCurrentFrame();
    }

}
