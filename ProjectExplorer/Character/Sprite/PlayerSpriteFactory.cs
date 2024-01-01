using ProjectExplorer.Items;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Sprite
{
    public class PlayerSpriteFactory
    {
        private static readonly PlayerSpriteFactory instance = new();

        private const float WALK_DELAY = 6 / 60f;

        private const int WALK_FRAMES = 4;
        private const int ATTACK_FRAMES = 4;

        public static PlayerSpriteFactory Instance
        {
            get { return instance; }
        }

        private PlayerSpriteFactory()
        {
        }

        private int GetOffset(Direction direction)
        {
            return direction switch
            {
                Direction.Down => 0,
                Direction.Right => 32,
                Direction.Up => 64,
                Direction.Left => 96,
                _ => throw new NotImplementedException(),
            };
        }

        public IAnimatedSprite GetPlayerMoveSprite(Direction direction, IPlayer player)
        {
            Rectangle source = new(0, GetOffset(direction), 16, 32);
            return new PlayerSubsprite(new SpriteDefinition("Character", source), player, WALK_FRAMES)
            {
                Delay = WALK_DELAY,
                Repeat = true,
            };
        }

        public IAnimatedSprite GetPlayerAttackingSprite(Direction direction, IPlayer player)
        {
            Rectangle source = new(32, GetOffset(direction) + 128, 32, 32);
            return new AttackSprite(new SpriteDefinition("Character", source), player);
        }

        public IAnimatedSprite GetPickupSprite(IPlayer player, IItem item, float duration = 1)
        {
            return new PickupSprite(player, item)
            {
                Duration = duration,
            };
        }
    }
}
