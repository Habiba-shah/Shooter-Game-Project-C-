using GameProjectOop.Core;
using GameProjectOop.Extensions;
using GameProjectOop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZInput;
using System.Threading.Tasks;

namespace GameProjectOop.Entities
{

    public class Enemy : GameObject
    {
        // Optional movement behavior: demonstrates composition and allows testable movement logic.
        public IMovement? Movement { get; set; }

        // Animation Frames
        public List<Image> LeftFrames { get; set; } = new List<Image>();
        public List<Image> RightFrames { get; set; } = new List<Image>();

        private int _currentFrameIndex = 0;
        private int _frameDelay = 5; // Adjust speed of animation
        private int _frameTimer = 0;

        // Default enemy velocity is set in constructor to give basic movement out-of-the-box.
        public Enemy()
        {
            Velocity = new PointF(-2, 0);
        }

        private PointF _previousPosition;

        /// Update will call movement behavior (if any) and then apply base update to move by velocity.
        public override void Update(GameTime gameTime)
        {
            _previousPosition = Position;

            Movement?.Move(this, gameTime); // movement must be called
            base.Update(gameTime);

            Animate();
        }

        private void Animate()
        {
            if (LeftFrames.Count == 0 || RightFrames.Count == 0) return;

            // Determine direction based on actual change in position
            float deltaX = Position.X - _previousPosition.X;

            // Only animate if moving
            if (Math.Abs(deltaX) > 0.1f)
            {
                _frameTimer++;
                if (_frameTimer >= _frameDelay)
                {
                    _frameTimer = 0;
                    _currentFrameIndex++;
                }

                if (deltaX < 0)
                {
                    // Moving Left
                    _currentFrameIndex %= LeftFrames.Count;
                    Sprite = LeftFrames[_currentFrameIndex];
                }
                else if (deltaX > 0)
                {
                    // Moving Right
                    _currentFrameIndex %= RightFrames.Count;
                    Sprite = RightFrames[_currentFrameIndex];
                }
            }
        }

        /// Custom draw: demonstrates polymorphism (override base draw to provide enemy visuals).
        public override void Draw(Graphics g)
        {
            if (Sprite != null)
            {
                g.DrawImage(Sprite, Bounds);
            }
            else
            {
                g.FillRectangle(Brushes.Red, Bounds);
            }
        }

        /// On collision, enemy deactivates when hit by bullets (encapsulation of reaction logic inside the entity).
        public override void OnCollision(GameObject other)
        {
            if (other is Bullet)
                IsActive = false;
        }
    }
}
