using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles
{
    public class LevelTransitionTrigger : IGameObject, ICollidable, IMitotic
    {
        private ICollisionHandler handler;
        private Rectangle collider;
        private Direction direction;
        public CollisionGroup Group => CollisionGroup.Triggers;

        public LevelTransitionTrigger(Point position, Point size, string level, Direction direction, Vector2? destination = null)
        {
            collider = new Rectangle(position, size);
            handler = new TransitionHandler(level, direction, destination);
            this.direction = direction;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Triggers don't have a sprite
        }

        public Rectangle GetCollider()
        {
            return collider;
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return handler;
        }

        public void OnRegister(ILevel level)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            // I'm not too happy with this.
            // TODO: Should probably break off the shortcut level transition triggers into their own thing and force destination on this one.
            Point size = objectDefinition.GetPoint("Width", "Height", collider.Size);
            string level = objectDefinition.GetValue<string>("Level");
            Direction direction = DirectionMethods.Parse(objectDefinition.GetValue("Direction", this.direction.ToString()));
            if (objectDefinition.ContainsKey("DestX") && objectDefinition.ContainsKey("DestY"))
            {
                Vector2 destination = objectDefinition.GetVector2("DestX", "DestY");
                return new LevelTransitionTrigger(objectDefinition.Position.ToPoint(), size, level, direction, destination);
            }
            else
            {
                return new LevelTransitionTrigger(objectDefinition.Position.ToPoint(), size, level, direction);
            }
        }
    }
}
