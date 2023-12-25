using ProjectExplorer.SpriteUtil;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ProjectExplorer.CharacterNS;
using System.Diagnostics;

namespace ProjectExplorer.UI
{
    public class Screen : IScreen
    {
        protected readonly IList<ISprite> modules;

        public Matrix Transform { get => Matrix.CreateTranslation(Position.X, Position.Y, 0);  }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public int Layer { get; set; } = 0;

        public Screen(Vector2 position, int layer = 0)
        {
            modules = new List<ISprite>();
            Position = position;
            Layer = layer;
        }

        public virtual void AddModule(ISprite module)
        {
            modules.Add(module);
        }

        public virtual void RemoveModule(ISprite module)
        {
            modules.Remove(module);
        }

        public virtual void Draw(GameTime gametime, SpriteBatch spriteBatch, Matrix transformation)
        {
            spriteBatch.Begin(blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                transformMatrix: Transform * transformation 
                );
            foreach (ISprite module in modules.ToArray())
            {
                module.Draw(gametime, spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
