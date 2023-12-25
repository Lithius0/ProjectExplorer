using ProjectExplorer.CharacterNS;
using ProjectExplorer.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Tiles.ControlTiles
{
    /// <summary>
    /// Factory to create all the various buttons and sliders needed.
    /// Also testing using a static class instead of a singleton.
    /// In theory, this class should be fine as is.
    /// </summary>
    public static class ControlTileFactory
    {

        public static Slider GetTestSlider(Vector2 position)
        {
            Slider slider = new(position, 5, 15);
            slider.Changed += (object sender, SliderValueArgs e) =>
            {
                Debug.WriteLine(e.NewValue);
            };
            return slider;
        }

        public static Slider GetSpeedSlider(Vector2 position)
        {
            Slider slider = new(position, "{0:f0}", Tiling.ToPixels(2), Tiling.ToPixels(8));
            slider.Value = PlayerConfig.MovementSpeed; ;
            slider.Changed += (object sender, SliderValueArgs e) =>
            {
                PlayerConfig.MovementSpeed = e.NewValue;
            };
            return slider;
        }
        public static Slider GetAttackSpeedSlider(Vector2 position)
        {
            float speed = 1 / PlayerConfig.AttackDuration;
            Slider slider = new(position, "{0:f0}", 1, 8);
            slider.Value = speed;
            slider.Changed += (object sender, SliderValueArgs e) =>
            {
                PlayerConfig.AttackDuration = 1 / e.NewValue;
            };
            return slider;
        }

        public static Button GetScaleUpButton(Vector2 position)
        {
            Button button = new(position);
            button.PressStart += (_, _) =>
            {
                Coordinator.Instance.ScreenManager.Scale++;
            };
            return button;
        }

        public static Button GetScaleDownButton(Vector2 position)
        {
            Button button = new(position);
            button.PressStart += (_, _) =>
            {
                Coordinator.Instance.ScreenManager.Scale--;
            };
            return button;
        }

        public static Slider GetTransitionTimeSlider(Vector2 position)
        {
            Slider slider = new(position, "{0:f0}", 0.0001f, 3);
            slider.Value = 1;
            slider.Changed += (object sender, SliderValueArgs e) =>
            {
                Coordinator.Instance.LevelManager.TransitionTime = e.NewValue;
            };
            return slider;
        }

        public static ToggleButton GetToggleInvicibilityButton(Vector2 position)
        {
            ToggleButton button = new(position);
            if (button.On != PlayerConfig.Invincible)
            {
                button.Toggle();
            }
            button.Toggled += (_, _) =>
            {
                PlayerConfig.Invincible = button.On;
            };
            return button;
        }

        public static Button GetHealButton(Vector2 position)
        {
            Button button = new(position);
            button.PressStart += (_, _) =>
            {
                Coordinator.Instance.LevelManager.Player.Heal(1000);
            };
            return button;
        }
        public static Button GetAddMaxHealthButton(Vector2 position)
        {
            Button button = new(position);
            button.PressStart += (_, _) =>
            {
                Coordinator.Instance.LevelManager.Player.AddMaxHealth(2);
            };
            return button;
        }

        public static ToggleButton GetSweepToggleButton(Vector2 position)
        {
            ToggleButton button = new(position);
            button.Toggled += (_, _) =>
            {
                PlayerConfig.Sweep = button.On;
            };
            return button;
        }
        public static ToggleButton GetHardModeButton(Vector2 position)
        {
            ToggleButton button = new(position);
            button.Toggled += (_, _) =>
            {
                Coordinator.Instance.HardMode = button.On;
            };
            return button;
        }
    }
}
