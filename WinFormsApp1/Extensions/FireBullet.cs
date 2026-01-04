using GameProjectOop.Core;
using GameProjectOop.Entities;
using System.Drawing;

namespace GameProjectOop.Extensions
{
    public class FireBullet : EnemyBullet
    {
        private static Image? _fireImage;

        public FireBullet()
        {
            Size = new SizeF(30, 30); // Fire is usually bigger than normal bullets
            Velocity = PointF.Empty;

            if (_fireImage == null)
            {
                Image original = Properties.Resources.fire;
                _fireImage = new Bitmap(original, (int)Size.Width, (int)Size.Height);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(Graphics g)
        {
            if (_fireImage != null)
            {
                g.DrawImage(_fireImage, Bounds);
            }
            else
            {
                g.FillEllipse(Brushes.OrangeRed, Bounds);
            }
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
