using GameProjectOop.Core;
using GameProjectOop.Entities;
using System.Drawing;

namespace GameProjectOop.Extensions
{
    public class EnemyBullet : GameObject
    {
        public EnemyBullet()
        {
            Size = new SizeF(10, 10);
            Velocity = PointF.Empty;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Black, Bounds);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                IsActive = false;
            }
        }
    }
}
