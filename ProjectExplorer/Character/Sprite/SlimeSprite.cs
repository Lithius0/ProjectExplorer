﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectExplorer.SpriteUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character.Sprite
{
    public class SlimeSprite : IAnimatedSprite
    {
        private ICharacter character;
        private Direction previousDirection;
        private IAnimatedSprite leftSprite;
        private IAnimatedSprite rightSprite;
        private IAnimatedSprite activeSprite;
        private bool playing = false;

        public SlimeSprite(ICharacter character)
        {
            this.character = character;
            previousDirection = character.Direction;
            leftSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 16, 16, 16), character, 3, 1 / 6f, true);
            rightSprite = new SimpleAnimatedSprite(SpriteManager.GetTexture("Slime"), new Rectangle(0, 0, 16, 16), character, 3, 1 / 6f, true);
            activeSprite = leftSprite;

            leftSprite.ReachedEnd += OnReachedEnd;
            rightSprite.ReachedEnd += OnReachedEnd;
        }

        public event EventHandler OnPlay;
        public event EventHandler OnStop;
        public event EventHandler OnPause;
        public event EventHandler ReachedEnd;

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Direction direction = character.Direction;
            if (direction != previousDirection)
            {
                activeSprite.Stop();
                switch (direction)
                {
                    case Direction.UP:
                    case Direction.LEFT:
                        activeSprite = leftSprite;
                        break;
                    case Direction.DOWN:
                    case Direction.RIGHT:
                        activeSprite = rightSprite;
                        break;
                }
                if (playing)
                {
                    activeSprite.Play();
                }
                previousDirection = direction;
            }
            activeSprite.Draw(gametime, spriteBatch);
        }

        public IAnimatedSprite Pause()
        {
            playing = false;
            activeSprite.Pause();
            OnPause?.Invoke(this, EventArgs.Empty);
            return this;
        }

        public IAnimatedSprite Play()
        {
            playing = true;
            activeSprite.Play();
            OnPause?.Invoke(this, EventArgs.Empty);
            return this;
        }

        public IAnimatedSprite Stop()
        {
            playing = false;
            activeSprite.Stop();
            OnStop?.Invoke(this, EventArgs.Empty);
            return this;
        }

        // Just a method to pass the reached end event.
        private void OnReachedEnd(object sender, EventArgs e)
        {
            ReachedEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}