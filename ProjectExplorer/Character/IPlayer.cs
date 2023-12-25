using ProjectExplorer.Items.Storage;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.CharacterNS
{
    public interface IPlayer : ICharacter
    {
        public IInventory Inventory { get; }
        public IItemSelector ItemSelector { get; }
        public PlayerStateMachine StateMachine { get; }

        /// <summary>
        /// Add to Player's max health
        /// </summary>
        public void AddMaxHealth(int maxHealth);
        /// <summary>
        /// Attempts to move Player in the given direction.
        /// </summary>
        public void Move(Direction direction);
        /// <summary>
        /// Stops attempting to move Player in the given direction.
        /// </summary>
        public void Stop(Direction direction);

        /// <summary>
        /// Use Player's primary item
        /// </summary>
        public void UsePrimary();
        /// <summary>
        /// Use Player's secondary item
        /// </summary>
        public void UseSecondary();
        /// <summary>
        /// Uses an item in the given selectable location.
        /// Method primarily for testing.
        /// </summary>
        /// <param name="location">Location of the item to use.</param>
        public void UseItem(int location);
        /// <summary>
        /// Resets Player to the original state
        /// </summary>
        public void Reset();
        /// <summary>
        /// Plays the given sprite. Player's sprite will be hidden while animation is player.
        /// </summary>
        /// <param name="sprite">The sprite to player</param>
        /// <param name="freezes">If true, player is frozen during animation</param>
        public void PlayAnimation(IAnimatedSprite sprite, bool freezes);
    }
}
