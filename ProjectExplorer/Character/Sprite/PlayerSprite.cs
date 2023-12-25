using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectExplorer.Character.Sprite
{
    /// <summary>
    /// Sprite for the player.
    /// This is a composite sprite and its primary purpose is to switch between its various subsprites.
    /// Although it sticks to player, it's not actually a stickable sprites, its subsprites are.
    /// </summary>
    class PlayerSprite : ISprite
    {
        private IPlayer player;
        // Subsprites
        private IDictionary<Direction, IAnimatedSprite> moveSprites;
        private IDictionary<Direction, IAnimatedSprite> attackSprites;

        public PlayerSprite(IPlayer player, ISticky sticky)
        {
            this.player = player;

            moveSprites = new Dictionary<Direction, IAnimatedSprite>
            {
                { Direction.UP, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.UP, player, sticky) },
                { Direction.DOWN, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.DOWN, player, sticky) },
                { Direction.LEFT, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.LEFT, player, sticky) },
                { Direction.RIGHT, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.RIGHT, player, sticky) }
            };
            attackSprites = new Dictionary<Direction, IAnimatedSprite>
            {
                { Direction.UP, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.UP, player, sticky) },
                { Direction.DOWN, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.DOWN, player, sticky) },
                { Direction.LEFT, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.LEFT, player, sticky) },
                { Direction.RIGHT, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.RIGHT, player, sticky) }
            };

            player.StateMachine.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object sender, StateChangedArgs args)
        {
            switch (args.NewState)
            {
                case PlayerState.Attacking:
                    attackSprites[player.Direction].Stop().Play();
                    break;
                case PlayerState.Idle:
                    moveSprites[player.Direction].Stop();
                    break;
                case PlayerState.Moving:
                    // Player can change direction while moving without updating state
                    // So all of them should be playing
                    foreach (IAnimatedSprite sprite in moveSprites.Values)
                    {
                        sprite.Play();
                    }
                    break;
            }
        }
        
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (player.StateMachine.State == PlayerState.Attacking)
                attackSprites[player.Direction].Draw(gametime, spriteBatch);
            else
                moveSprites[player.Direction].Draw(gametime, spriteBatch);
        }
    }
}
