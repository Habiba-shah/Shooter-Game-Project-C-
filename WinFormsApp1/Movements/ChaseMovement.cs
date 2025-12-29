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
    public class ChaseMovement : IMovement
    {
        private GameObject target;
        private float speed;

        public ChaseMovement(GameObject target, float speed)
        {
            this.target = target;
            this.speed = speed;
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            Console.WriteLine(target == null);

            float dx = target.Position.X - obj.Position.X;
            float dy = target.Position.Y - obj.Position.Y;

            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            if (length == 0) return;

            dx /= length;
            dy /= length;

            obj.Position = new PointF(
                obj.Position.X + dx * speed,
                obj.Position.Y + dy * speed
            );
        }
    }
}


