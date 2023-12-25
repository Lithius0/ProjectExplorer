using ProjectExplorer.Collision;
using ProjectExplorer.DebugMode;
using ProjectExplorer.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil;

namespace ProjectExplorer.DebugMode
{
    /// <summary>
    /// Utility class for drawing debug objects to visualize the location of invisible things.
    /// Is not connected to <see cref="DebugInfo"/> because this class is drawn with transformations.
    /// </summary>
    public class DebugColliders
    {
        private static readonly DebugColliders instance = new();
        private bool enabled = false;

        public static DebugColliders Instance
        { get { return instance; } }

        private DebugColliders() { }

        public void DrawBox(SpriteBatch spriteBatch, Rectangle transform, Color color)
        {
            if (!enabled) return;

            spriteBatch.Draw(SpriteManager.GetTexture("Pixel"), transform, null, color, 0, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public void DrawCollider(SpriteBatch spriteBatch, ICollidable collidable)
        {
            if (!enabled) return;

            DrawBox(spriteBatch, collidable.GetCollider(), collidable.Group.GetDebugColor() * 0.5f);
        }

        public void Toggle()
        {
            enabled = !enabled;
        }
    }
}
