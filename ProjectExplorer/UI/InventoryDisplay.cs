using ProjectExplorer.SpriteUtil;
using ProjectExplorer.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using ProjectExplorer.Character;
using System.Collections.Generic;
using System;
using ProjectExplorer.Items;
using ProjectExplorer.Items.Storage;
using ProjectExplorer.Levels;
using ProjectExplorer.Items.Sprites;
using ProjectExplorer.Projectiles;
using ProjectExplorer.Projectiles.Sprites;
using ProjectExplorer;
using Microsoft.Xna.Framework.Input;
using ProjectExplorer.Controllers;

public class InventoryDisplay : ISprite
{
    private static readonly int Columns = 4;
    private static readonly Vector2 Spacing = new(16, 16);
    private static readonly Vector2 SelectablesOffset = new(136, 56);
    private static readonly Vector2 SelectorOffset = new(128, 48);

    private IPlayer player;
    private ISprite TopInventory;
    private ISprite BottomInventory;
    private ISprite compass;
    private ISprite map;
    private ISprite secondary;
    private ISprite itemSelect;
    private ISprite itemSelectAlt;
    private ISprite boss;

    private IInventory inventory;
    private LevelManager levelManager;
    private ISprite playerDot;
    private Vector2 playerPositionOnMap;
    private bool flash = true;
    private float flashTimer = 0f;
    private float flashInterval = 0.25f;


    public InventoryDisplay(IPlayer player, LevelManager currentLevel)
    {
        this.player = player;
        this.levelManager = currentLevel;
    }

    private static Vector2 Wrap(int slot)
    {
        return new Vector2(slot % Columns, slot / Columns);
    }
 
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        secondary = player.ItemSelector.Secondary?.GetSprite(new Vector2(72, 56));

        foreach (KeyValuePair<int, IItem> slot in player.ItemSelector.Selectables)
        {
            Vector2 position = Wrap(slot.Key) * Spacing;
            position += SelectablesOffset;

            slot.Value?.GetSprite(position).Draw(gameTime, spriteBatch);

        }
    }
}