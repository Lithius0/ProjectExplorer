using ProjectExplorer.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Character;

namespace ProjectExplorer.Collision
{
    public class ExplosionCollision : ICollisionHandler
    {
        // Make sure character isn't hit by the same explosion twice
        // Multiple explosion objects can be linked to the same handler to create a larger composite explosion.
        private ISet<ICollidable> hit;
        private int damage;
        
        public ExplosionCollision(int damage = 1)
        {
            this.damage = damage;
            hit = new HashSet<ICollidable>();
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            if (!hit.Contains(other))
            {
                hit.Add(other);

                if (other is ICharacter character)
                {
                    character.Damage(damage, Vector2.Zero);
                }
                else if (other is IExplodable explodable)
                {
                    explodable.Explode();
                }
            }
        }
    }
}
