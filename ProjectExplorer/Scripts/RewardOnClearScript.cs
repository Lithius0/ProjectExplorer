﻿using ProjectExplorer.Items;
using ProjectExplorer.Levels;
using ProjectExplorer.SoundEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Character.Enemies;

namespace ProjectExplorer.Scripts
{
    public class RewardOnClearScript : IGameObject
    {
        private IGameObject obj;
        private ILevel level;
        public Vector2 Position { get; set; }

        public RewardOnClearScript(IGameObject obj)
        {
            this.obj = obj;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        public void OnRegister(ILevel level)
        {
            this.level = level;
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Change this to the IEnemy interface
            ISet<Enemy> enemies = level.GetObjects<Enemy>();
            if (enemies.Count <= 0)
            {
                SoundFactory.Instance.PlaySound("KeyAppear");
                level.Deregister(this);
                level.Register(obj);
            }
        }
    }
}
