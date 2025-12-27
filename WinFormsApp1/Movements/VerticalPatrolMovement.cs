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
    public class VerticalPatrolMovement : IMovement
    {
        private float speed;
        private float topBound;
        private float bottomBound;

        public VerticalPatrolMovement(float speed, float topBound, float bottomBound)
        {
            this.speed = speed;
            this.topBound = topBound;
            this.bottomBound = bottomBound;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            obj.Position = new PointF(obj.Position.X, obj.Position.Y + speed);

            if (obj.Position.Y <= topBound || obj.Position.Y >= bottomBound)
                speed = -speed;
        }
    }
}