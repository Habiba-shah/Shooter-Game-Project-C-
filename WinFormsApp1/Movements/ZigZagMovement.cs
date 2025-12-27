using EZInput;
using GameProjectOop.Core;
using GameProjectOop.Entities;
using GameProjectOop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Movements
{
    public class ZigZagMovement : IMovement
    {
        private float speedX;
        private float amplitude;
        private float frequency;
        private float time;

        public ZigZagMovement(float speedX, float amplitude, float frequency)
        {
            this.speedX = speedX;
            this.amplitude = amplitude;
            this.frequency = frequency;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            time += frequency;
            float yOffset = (float)Math.Sin(time) * amplitude;

            obj.Position = new PointF(
                obj.Position.X + speedX,
                obj.Position.Y + yOffset
            );
        }
    }
}

