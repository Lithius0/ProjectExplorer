using ProjectExplorer.CharacterNS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Collision
{
    public class TransitionHandler : ICollisionHandler
    {
        private string level;
        private Direction direction;
        private Vector2? destination;

        public event EventHandler OnChangeLevelBefore;
        public event EventHandler OnChangeLevelAfter;

        public string Level
        {
            get { return level; }
            set { level = value; }
        }

        public TransitionHandler(string level, Direction direction, Vector2? destination)
        {
            this.level = level;
            this.direction = direction;
            this.destination = destination;
        }

        public void Collide(ICollidable other, Rectangle intersection)
        {
            if (other is IPlayer player)
            {
                OnChangeLevelBefore?.Invoke(this, EventArgs.Empty);

                player.Level.Manager?.ChangeLevel(level, direction, destination, null);

                OnChangeLevelAfter?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
