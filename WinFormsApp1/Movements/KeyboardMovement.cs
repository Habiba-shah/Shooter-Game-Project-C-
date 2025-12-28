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
    public class KeyboardMovement : IMovement
    {
        public float Speed { get; set; } = 5f;

        public RectangleF Bounds { get; set; }
        public void Move(GameObject obj, GameTime gameTime)
        {
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
                obj.Position = new PointF(obj.Position.X - Speed, obj.Position.Y);

            if (Keyboard.IsKeyPressed(Key.RightArrow))
                obj.Position = new PointF(obj.Position.X + Speed, obj.Position.Y);

            if (Keyboard.IsKeyPressed(Key.UpArrow))
                obj.Position = new PointF(obj.Position.X, obj.Position.Y - Speed);

            if (Keyboard.IsKeyPressed(Key.DownArrow))
                obj.Position = new PointF(obj.Position.X, obj.Position.Y + Speed);

            float x = obj.Position.X;
            float y = obj.Position.Y;

            // clamp X
            if (x < 0) x = 0;
            if (x > 1000 - obj.Size.Width) x = 1000 - obj.Size.Width;

            // clamp Y
            if (y < 0) y = 0;
            if (y > 600 - obj.Size.Height) y = 600 - obj.Size.Height;

            obj.Position = new PointF(x, y);

        }



    }

}
