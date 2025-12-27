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
    public class JumpMovement : IMovement
    {
        private float jumpForce;
        private float gravity;
        private float velocityY;
        private bool isGrounded;

        public JumpMovement(float jumpForce, float gravity)
        {
            this.jumpForce = jumpForce;
            this.gravity = gravity;
            isGrounded = true;
        }

        public void Jump()
        {
            if (isGrounded)
            {
                velocityY = -jumpForce;
                isGrounded = false;
            }
        }

        public void Move(GameObject obj, GameTime gameTime)
        {
            velocityY += gravity;
            obj.Position = new PointF(obj.Position.X, obj.Position.Y + velocityY);

            if (obj.Position.Y >= 400) // ground level
            {
                obj.Position = new PointF(obj.Position.X, 400);
                velocityY = 0;
                isGrounded = true;
            }
        }
    }

}
