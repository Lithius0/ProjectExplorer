﻿using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.CharacterNS;
using ProjectExplorer.SoundEffects;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Items.ItemEntities
{
    /// <summary>
    /// The physical representation of an item as a object.
    /// By default, objects of this class just sit there as an object waiting for a collision.
    /// </summary>
    public class ItemEntity : IGameObject, ICollidable, IItemDispenser, ISticky, IMitotic
    {
        protected int amount;
        protected IItem item;
        protected Vector2 position;
        protected readonly Vector2 initialPosition;
        protected ISprite sprite;
        protected Point size = Tiling.Full;

        protected ILevel level;

        protected ICollisionHandler collisionHandler;

        public Vector2 Position
        { get { return position; } }

        public CollisionGroup Group => CollisionGroup.Items;

        /// <summary>
        /// Empty constructor for inherited classes to use.
        /// </summary>
        protected ItemEntity()
        {

        }

        public ItemEntity(IItem item, Vector2 position, Point size, int amount = 1) 
        {
            sprite = item.GetSprite(position);
            if (sprite is IStickable stickable)
            {
                stickable.StickTo(this);
            }
            this.position = position;
            this.size = size;
            this.item = item;
            this.amount = amount;
            collisionHandler = new ItemDispenserCollision(this);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite?.Draw(gameTime, spriteBatch);
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Dispense(IPlayer player)
        {
            item.Pickup(player, amount);
            SoundFactory.Instance.PlaySound("GetItem");
            // Most items just remove themselves after being picked up.
            level?.Deregister(this);
        }

        public Rectangle GetCollider()
        {
            return Positioning.ConstructFromAnchorPoint(position, size, AnchorPoints.Middle);
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return collisionHandler;
        }

        public IGameObject Clone(ObjectDefinition objectDefinition)
        {
            return new ItemEntity(item, objectDefinition.Position, size, objectDefinition.GetValue("Amount", amount));
        }
    }
}
