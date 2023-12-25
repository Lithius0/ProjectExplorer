using ProjectExplorer.SpriteUtil;
using ProjectExplorer.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using ProjectExplorer.Character;
using System.Collections.Generic;
using System;
using ProjectExplorer;
using ProjectExplorer;
using ProjectExplorer.UI;
using ProjectExplorer.UI.Elements;

public class HealthCount : ISprite
{
    private Vector2 offset = new(16, 16);
    private Vector2 position;
    private int columns;
    private int max;

    public HealthCount(Vector2 position, int columns = 8, int max = 16)
    {
        this.position = position;
        this.columns = columns;
        this.max = max;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        IPlayer player = Coordinator.Instance.Player;

        for (int i = 0; i < Math.Ceiling(player.MaxHealth) && i < max; i++)
        {
            Vector2 heartPosition = new Vector2(i % columns, MathF.Floor(i / columns)) * offset + position;
            float fullness = player.Health - i;
            // TODO: See if it's possible to draw this without instantiating new objects every Draw call.
            new HeartDisplaySprite(heartPosition, fullness).Draw(gameTime, spriteBatch);
        }
    }
}