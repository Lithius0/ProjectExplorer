using ProjectExplorer.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.Utility;

namespace ProjectExplorer.Collision
{
    /// <summary>
    /// Handles projectile collisions. Mostly just damages characters when they come into contact.
    /// </summary>
    public class ProjectileCollisionHandler : ICollisionHandler
    {
        private Projectile projectile;
        private bool removeOnHit;
        private ISet<ICharacter> hit;
        private bool block = false;
        // Handler will ignore objects of this group.
        // Usually this is player/enemy.
        private CollisionGroup ownerGroup = CollisionGroup.Players;

        public ProjectileCollisionHandler(Projectile projectile, bool removeOnHit = true)
        {
            this.projectile = projectile;
            this.removeOnHit = removeOnHit;
            hit = new HashSet<ICharacter>();
            if (projectile.Owner is ICollidable owner)
            {
                ownerGroup = owner.Group;
            }
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            // Hitting characters
            if (other is ICharacter character && ownerGroup != other.Group && !hit.Contains(character))
            {
                hit.Add(character);

                Vector2 pushVelocity = projectile.Velocity;
                // Just making sure not to get a NaN here
                if (pushVelocity.LengthSquared() > 0.01f)
                {
                    // Rescale so all projectiles push the same amount.
                    pushVelocity.Normalize();
                    pushVelocity *= Tiling.ToPixels(2);
                }

                if (projectile.Owner is not IPlayer)
                {
                    CheckBlock(character);
                }
                if (!block)
                {
                    character.Damage(projectile.Damage, pushVelocity);
                }
                if (removeOnHit)
                    projectile.Remove();
            }
            // Hitting walls
            else if (other.Group == CollisionGroup.Tiles && removeOnHit)
            {
                projectile.Remove();
            }
        }

        private void CheckBlock(ICharacter character)
        {
            bool isProjectileMovingLeft = projectile.Velocity.X < 0;
            bool isProjectileMovingRight = projectile.Velocity.X > 0;
            bool isProjectileMovingUp = projectile.Velocity.Y > 0;
            bool isProjectileMovingDown = projectile.Velocity.Y < 0;

            bool characterFacingRight = character.Direction == Direction.RIGHT;
            bool characterFacingLeft = character.Direction == Direction.LEFT;
            bool characterFacingUp = character.Direction == Direction.UP;
            bool characterFacingDown = character.Direction == Direction.DOWN;

            block = (isProjectileMovingLeft && (characterFacingRight || (isProjectileMovingUp && characterFacingUp) || (isProjectileMovingDown && characterFacingDown)))
                 || (isProjectileMovingRight && (characterFacingLeft || (isProjectileMovingUp && characterFacingUp) || (isProjectileMovingDown && characterFacingDown)))
                 || (isProjectileMovingDown && characterFacingDown)
                 || (isProjectileMovingUp && characterFacingUp);
        }
    }
}
