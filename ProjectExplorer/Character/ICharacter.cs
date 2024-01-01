using ProjectExplorer.Levels;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil;

namespace ProjectExplorer.Character
{
    public interface ICharacter : IGameObject
    {
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public Direction Direction { get; set; }
        public ILevel Level { get; }
        public bool RecentlyDamaged { get; }
        /// <summary>
        /// Attempt to damage this character.
        /// </summary>
        /// <param name="damage">Amount of damage to deal</param>
        /// <param name="push">Amount of "push" for the damage. 
        /// Effectively moves the character that distance if damaged.</param>
        /// <returns>True if the character has been damaged, false otherwise.</returns>
        public bool Damage(int damage, Vector2 push);
        public void Heal(int heal);
        /// <summary>
        /// Moves the player by a given amount
        /// </summary>
        /// <param name="move">The amount to move the player by</param>
        public void Move(Vector2 move);
        public Rectangle GetTransform();

        public event EventHandler Died;
    }
}
