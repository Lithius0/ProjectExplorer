using ProjectExplorer.Items.Sprites;
using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Projectiles.Sprites
{
    public class ProjectileSpriteFactory
    {
        private static ProjectileSpriteFactory instance = new ProjectileSpriteFactory();

        public static ProjectileSpriteFactory Instance
        {
            get { return instance; }
        }

        private ProjectileSpriteFactory()
        {
        }


        public ISprite GetArrowSprite(Arrow arrow)
        {
            return new ArrowSprite(new SpriteDefinition("Projectiles", 0, 0, 16, 16), arrow);
        }
        public ISprite GetSwordSweepSprite(SwordSweep sword)
        {
            return new SwordSweepSprite(sword);
        }
    }
}
