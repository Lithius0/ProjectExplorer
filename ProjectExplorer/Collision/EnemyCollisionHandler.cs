using ProjectExplorer.Character;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Utility;
using ProjectExplorer.SoundEffects;

namespace ProjectExplorer.Collision
{
    public class EnemyCollisionHandler : ICollisionHandler
    {

        public void Collide(ICollidable other, Rectangle intersection)
        {
            if (other is IPlayer player)
            {
                Rectangle collider = other.GetCollider();
                Vector2 push = (collider.Center - intersection.Center).ToVector2();
                // Making sure the push only happens along the cardinal directions.
                if (Math.Abs(push.X) > Math.Abs(push.Y))
                    push.Y = 0;
                else
                    push.X = 0;

                if (push.LengthSquared() > 0.01)
                {
                    push.Normalize();
                    push *= Tiling.ToPixels(2);

                }
                player.Damage(1, push);
            }
        }
    }
}
