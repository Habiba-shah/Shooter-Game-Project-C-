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

            // clamp X using REAL screen bounds
            if (x < Bounds.Left)
                x = Bounds.Left;

            if (x + obj.Size.Width > Bounds.Right)
                x = Bounds.Right - obj.Size.Width;

            // clamp Y using REAL screen bounds
            if (y < Bounds.Top)
                y = Bounds.Top;

            if (y + obj.Size.Height > Bounds.Bottom)
                y = Bounds.Bottom - obj.Size.Height;


            obj.Position = new PointF(x, y);

        }



    }

}
