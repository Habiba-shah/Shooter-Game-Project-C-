using EZInput;
using GameProjectOop.Core;
using GameProjectOop.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Extensions
{

    public class Bullet : GameObject
    {
        // Bullets set a default velocity in the constructor - a simple example of behavior initialization.
        public Bullet()
        {
            Velocity = new PointF(8, 0);
        }

        /// Bullets use the default movement logic (base.Update) and deactivate when off-screen.
        /// Consider extending with continous collision detection (CCD) to avoid tunnelling at high speeds.
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// Simple visual representation for bullets (polymorphism example).
        public override void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, Bounds);
        }

        /// On collision bullets deactivate when hitting an enemy.
        /// Keep collision reaction encapsulated in the object class.
        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
                IsActive = false;
        }
    }
}