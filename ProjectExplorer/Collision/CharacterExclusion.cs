using ProjectExplorer.Levels;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Character;

namespace ProjectExplorer.Collision
{
    public class CharacterExclusion : IGameObject, ICollidable, IExclusion
    {
        private ILevel level;
        private ICharacter character;

        public CollisionGroup Group => CollisionGroup.Triggers;

        public CharacterExclusion(ICharacter character)
        {
            this.character = character;

            character.Died += OnCharacterDeath;
        }

        public void Exclude(Rectangle intersection)
        {
            Direction dir = character.Direction;
            Vector2 push = -dir.GetVector2();
            push *= intersection.Size.ToVector2();
            character.Move(push);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public virtual Rectangle GetCollider()
        {
            Rectangle transform = character.GetTransform();
            transform.Inflate(-1, -1);
            return new Rectangle(transform.X, transform.Y + transform.Height / 2, transform.Width, transform.Height / 2);
        }

        public ICollisionHandler GetCollisionHandler()
        {
            return EmptyCollisionHandler.Instance;
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public void Remove()
        {
            level?.Deregister(this);
        }
        protected void OnCharacterDeath(object sender, EventArgs e)
        {
            Remove();
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
