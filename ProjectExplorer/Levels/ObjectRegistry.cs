using ProjectExplorer.Items.ItemEntities;
using ProjectExplorer.Tiles.TileClasses;
using ProjectExplorer.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectExplorer.Utility;
using ProjectExplorer.Items;
using ProjectExplorer.Tiles.ControlTiles;
using ProjectExplorer.Scripts;
using ProjectExplorer.SpriteUtil.Text;
using ProjectExplorer.SpriteUtil;

namespace ProjectExplorer.Levels
{
    /// <summary>
    /// Relates the id of an object to a constructor or factory.
    /// Primary method of creating objects from their id's.
    /// </summary>
    public static class ObjectRegistry
    {
        private static readonly Dictionary<string, IMitotic> registry = new();

        // Whole bunch of helper methods
        private static Func<ObjectDefinition, IGameObject> Standardize(Func<Vector2, IGameObject> func)
        {
            return (ObjectDefinition def) => func(def.Position);
        }

        public static void Register()
        {
            // Tiles
            registry["grass"] = new BackgroundTile(Vector2.Zero, "Overworld", new Rectangle(0, 0, 16, 16));
            registry["cliff_nw"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0, 176, 16, 16));
            registry["cliff_n"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(16, 176, 16, 16));
            registry["cliff_ne"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(32, 176, 16, 16));

            registry["cliff_w"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0x40, 0xA0, 16, 16));
            registry["cliff_sw"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0x40, 0xB0, 16, 16));
            registry["cliff_s"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0x50, 0xB0, 16, 16));
            registry["cliff_se"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0x60, 0xB0, 16, 16));
            registry["cliff_e"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(0x60, 0xA0, 16, 16));

            registry["cliff_bottom"] = new ForegroundTile(Vector2.Zero, "Overworld", new Rectangle(80, 224, 16, 16));

            registry["text"] = new TextTile("DuskB3", Vector2.Zero, "Sample Text");

            // ExplodableTile
            registry["explodable"] = new ExplodableTile(new ForegroundTile(new Rectangle(0, 0, 16, 16), BaseSprite.GetMissingSprite(Vector2.Zero)));

            // Items
            registry["bomb"] = new ItemEntity(Bomb.Instance, Vector2.Zero, Tiling.Full);
            registry["heart"] = new ItemEntity(Heart.Instance, Vector2.Zero, Tiling.Full);
            registry["heart_container"] = new ItemEntity(HeartContainer.Instance, Vector2.Zero, Tiling.Full);
            registry["coin"] = new ItemEntity(Coin.Instance, Vector2.Zero, Tiling.Full);
            registry["starter_sword"] = new ItemEntity(StarterSword.Instance, Vector2.Zero, Tiling.Full);
            registry["key"] = new ItemEntity(Key.Instance, Vector2.Zero, Tiling.Full);
            registry["bow"] = new ItemEntity(Bow.Instance, Vector2.Zero, Tiling.Full);


            // Triggers
            registry["level_transition"] = new LevelTransitionTrigger(Point.Zero, Tiling.Full, "", Direction.UP);
            // TODO: Level transition trigger shortcuts
            // TODO: Controls

            // Scripts
            // TODO: Scripts
        }

        public static IGameObject GetObject(ObjectDefinition def)
        {
            return registry[def.ObjectId].Clone(def);
        }
    }
}
