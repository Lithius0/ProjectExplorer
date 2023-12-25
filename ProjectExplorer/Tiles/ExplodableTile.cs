using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles
{
    /// <summary>
    /// Tiles which disappear when exploded.
    /// Typically for a secret.
    /// </summary>
    public class ExplodableTile : IGameObject, ICollidable, IExplodable, IMitotic
    {
        private ILevel level;
        private ForegroundTile tile;

        // Objects in level with linkedLevelId with linkedTag will also be removed if this one is exploded
        private string linkedLevelId;
        private string linkedTag;
        public CollisionGroup Group => tile.Group;

        public ExplodableTile(ForegroundTile tile, string linkedLevelId, string linkedTag)
        {
            this.tile = tile;
            this.linkedLevelId = linkedLevelId;
            this.linkedTag = linkedTag;
        }
        public ExplodableTile(ForegroundTile tile)
        {
            this.tile = tile;
            linkedLevelId = null;
            linkedTag = null;
        }

        public void Explode()
        {
            level.Deregister(this);
            // Deregistering all linked objects.
            if (linkedLevelId != null && linkedTag != null)
            {
                ILevel linkedLevel = level.Manager.GetLevel(linkedLevelId);
                IGameObject[] tagged = linkedLevel.GetObjectsWithTag(linkedTag);
                foreach (IGameObject obj in tagged)
                {
                    linkedLevel.Deregister(obj);
                }
            }
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public void Update(GameTime gameTime)
        {
            tile.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            tile.Draw(gameTime, spriteBatch);
        }

        public Rectangle GetCollider()
        {
            return tile.GetCollider();
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return tile.GetCollisionHandler();
        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            objectDefinition.ObjectId = objectDefinition.GetValue<string>("Tile");
            IGameObject obj= (ForegroundTile)ObjectRegistry.GetObject(objectDefinition);
            if (obj is ForegroundTile tile)
            {
                // Using null as a default allows for handling non-existent fields. Using the other overload throws an exception.
                string linkedLevel = objectDefinition.GetValue<string>("LinkedLevel", null);
                string linkedTag = objectDefinition.GetValue<string>("LinkedTag", null);
                if (linkedLevel != null && linkedTag != null)
                {
                    return new ExplodableTile(tile, linkedLevel, linkedTag);
                }
                else
                {
                    return new ExplodableTile(tile);
                }
            }
            else
            {
                throw new ArgumentException("Tile must have an ObjectId referencing a ForegroundTile!");
            }
        }
    }
}
