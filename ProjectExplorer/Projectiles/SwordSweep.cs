using ProjectExplorer.Collision;
using ProjectExplorer.Items;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Character;
using ProjectExplorer.Projectiles.Sprites;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Projectiles
{
    /// <summary>
    /// Responsible for the sword which projects from the player during a sword swing.
    /// This class is used for all the different swords, as their thrust behavior is identical
    /// </summary>
    public class SwordSweep : Projectile
    {
        private float age = 0;
        private Vector2 offset;
        private float sweepAngle = MathHelper.ToRadians(180 / 2f);
        private float offsetLength = Tiling.ToPixels(3/4f);
        private float angle = 0;

        public float Angle => angle;

        public SwordSweep(Direction direction, ICharacter owner) : base(Vector2.Zero, direction.GetVector2(), owner)
        {
            sprite = ProjectileSpriteFactory.Instance.GetSwordSweepSprite(this);
            collisionHandler = new ProjectileCollisionHandler(this, false);
            offset = direction.GetVector2() * offsetLength;
            // Initial positioning of the sword.
            angle = -sweepAngle;
            Position = owner.Position + Positioning.Rotate(offset, angle);
            removeAfterDraw = true;
        }

        public override void Update(GameTime gameTime)
        {
            angle = MathHelper.Lerp(-sweepAngle, sweepAngle, age / PlayerConfig.AttackDuration);
            Position = owner.Position + Positioning.Rotate(offset, angle);
            age += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (age > PlayerConfig.AttackDuration)
                Remove();
        }
    }
}
