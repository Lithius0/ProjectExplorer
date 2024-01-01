using ProjectExplorer.Collision;
using ProjectExplorer.Levels;
using ProjectExplorer.Character;
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
    public class ItemEntity : IGameObject, ICollidable, IItemDispenser, IMitotic
    {
        protected int amount;
        protected IItem item;
        protected readonly Vector2 initialPosition;
        protected ISprite sprite;
        protected Point size = Tiling.Full;

        protected ILevel level;

        protected ICollisionHandler collisionHandler;

        public Vector2 Position { get; set; }

        public CollisionGroup Group => CollisionGroup.Items;

        /// <summary>
        /// Empty constructor for inherited classes to use.
        /// </summary>
        protected ItemEntity()
        {

        }

        public ItemEntity(IItem item, Vector2 position, Point size, int amount = 1) 
        {
            sprite = new BaseSprite(item.GetSprite())
            {
                Layer = LayerConstants.Item,
                AttachedObject = this,
            };
            Position = position;
            this.size = size;
            this.item = item;
            this.amount = amount;
            collisionHandler = new ItemDispenserCollision(this);
        }
        public ItemEntity(IItem item, Vector2 position, int amount = 1) : this(item, position, Tiling.Full, amount)
        {
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
            return AnchorPoints.Construct(Position, size, AnchorPoints.Middle);
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
