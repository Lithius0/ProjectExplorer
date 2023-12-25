using ProjectExplorer;
using ProjectExplorer.Items;
using ProjectExplorer.Items.Storage;
using ProjectExplorer.Levels;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil.Text;
using System.Diagnostics;

public class CoinCount : ISprite
{
    private Vector2 position;
    private TextSprite amountDisplay;

    public CoinCount(Vector2 position)
    {
        this.position = position;
        amountDisplay = new TextSprite("DuskB3", position, "");
    }

    public void Draw(GameTime gametime, SpriteBatch spriteBatch)
    {
        Coin.Instance.GetSprite(position).Draw(gametime, spriteBatch);

        int coins = Coordinator.Instance.Player.Inventory.AmountOf(Coin.Instance);

        amountDisplay.Text = coins.ToString();
        amountDisplay.Draw(gametime, spriteBatch);
    }
}