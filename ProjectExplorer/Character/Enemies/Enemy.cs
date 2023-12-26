using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectExplorer.Collision;
using ProjectExplorer.Items.ItemEntities;
using ProjectExplorer.Items;
using ProjectExplorer.Levels;
using ProjectExplorer.SoundEffects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Enemies
{
    public abstract class Enemy : BaseCharacter
    {
        protected int damage = 1;
        protected ICollisionHandler collisionHandler;

        protected DropTable dropTable;

        public override CollisionGroup Group => CollisionGroup.Enemies;

        public Enemy()
        {
            dropTable = new DropTable(3);
            dropTable.Add(Coin.Instance, 1, 4);
            dropTable.Add(Heart.Instance, 1, 1);

            collisionHandler = new EnemyCollisionHandler();
        }

        public override ICollisionHandler GetCollisionHandler()
        {
            return collisionHandler;
        }

        public override void Kill()
        {
            base.Kill();
            dropTable.Drop(level, position);
            level.Deregister(this);
        }
    }
}
