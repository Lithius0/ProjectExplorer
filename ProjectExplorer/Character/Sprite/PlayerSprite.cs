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

        public PlayerSprite(IPlayer player)
        {
            this.player = player;

            moveSprites = new Dictionary<Direction, IAnimatedSprite>
            {
                { Direction.Up, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.Up, player) },
                { Direction.Down, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.Down, player) },
                { Direction.Left, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.Left, player) },
                { Direction.Right, PlayerSpriteFactory.Instance.GetPlayerMoveSprite(Direction.Right, player) }
            };
            attackSprites = new Dictionary<Direction, IAnimatedSprite>
            {
                { Direction.Up, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.Up, player) },
                { Direction.Down, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.Down, player) },
                { Direction.Left, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.Left, player) },
                { Direction.Right, PlayerSpriteFactory.Instance.GetPlayerAttackingSprite(Direction.Right, player) }
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
