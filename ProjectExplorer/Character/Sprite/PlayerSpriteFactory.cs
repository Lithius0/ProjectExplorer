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

namespace ProjectExplorer.CharacterNS.Sprite
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
                Direction.DOWN => 0,
                Direction.RIGHT => 32,
                Direction.UP => 64,
                Direction.LEFT => 96,
                _ => throw new NotImplementedException(),
            };
        }

        public IAnimatedSprite GetPlayerMoveSprite(Direction direction, IPlayer player, ISticky sticky)
        {
            Rectangle source = new(0, GetOffset(direction), 16, 32);
            return new PlayerSubsprite(SpriteManager.GetTexture("Character"), source, sticky, player, WALK_FRAMES, WALK_DELAY, true);
        }

        public IAnimatedSprite GetPlayerAttackingSprite(Direction direction, IPlayer player, ISticky sticky)
        {
            Rectangle source = new(32, GetOffset(direction) + 128, 32, 32);
            return new AttackSprite(SpriteManager.GetTexture("Character"), source, sticky, player);
        }

        public IAnimatedSprite GetPickupSprite(Vector2 position, IItem item, float delay = 1, bool twoHanded = false)
        {
            return new PickupSprite(SpriteManager.GetTexture("Character"), position, item, delay, twoHanded);
        }
    }
}
