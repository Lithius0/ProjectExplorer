using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ProjectExplorer.Character.Sprite;
using ProjectExplorer.Collision;
using ProjectExplorer.Items;
using ProjectExplorer.Items.Storage;
using ProjectExplorer.SpriteUtil;
using ProjectExplorer.SoundEffects;
using Microsoft.VisualBasic;
using System;

namespace ProjectExplorer.Character
{
    class Player : BaseCharacter, IPlayer
    {
        private readonly IInventory inventory;
        private readonly IItemSelector selector;
        private readonly ICollisionHandler collisionHandler;

        private readonly PlayerStateMachine stateMachine;

        private ISprite sprite;
        private IAnimatedSprite animation; // Should be null except when there's a custom animation playing

        protected IList<Direction> movementList;

        public override CollisionGroup Group => CollisionGroup.Players;
        public IInventory Inventory => inventory;
        public IItemSelector ItemSelector => selector;
        public PlayerStateMachine StateMachine => stateMachine;

        public Player(Vector2 position)
        {
            this.position = position;

            inventory = new Inventory();
            selector = new ItemSelector(inventory);
            stateMachine = new PlayerStateMachine();
            movementList = new List<Direction>();
            collisionHandler = new PlayerCollision(this);

            // Sprite requires the state machine and needs to come afterwards
            sprite = new PlayerSprite(this);

            maxHealth = PlayerConfig.StartingHealth;
            Health = PlayerConfig.StartingHealth;

            Died += (_, _) =>
            {
                //PlayAnimation(new DeathSprite(this), true);
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (stateMachine.State != PlayerState.Locked)
            {
                if (movementList.Count > 0 && stateMachine.CanMove)
                {
                    direction = movementList[movementList.Count - 1];
                    stateMachine.ChangeState(PlayerState.Moving);
                }
                else
                {
                    stateMachine.ChangeState(PlayerState.Idle);
                }
                MoveUpdate(gameTime);
            }

            stateMachine.Update(gameTime);

            base.Update(gameTime);
        }

        public override bool Damage(int damage, Vector2 push)
        {
            if (PlayerConfig.Invincible)
            {
                return false;
            }
            else
            {
                if (Coordinator.Instance.HardMode)
                    damage *= 2;

                bool damaged = base.Damage(damage, push);
                // TODO: Damaged sound

                return damaged;
            }
        }

        private void MoveUpdate(GameTime gameTime)
        {
            if (stateMachine.State == PlayerState.Moving)
            {
                Vector2 changeInPosition = direction.ToVector2();
                changeInPosition *= (float)(PlayerConfig.MovementSpeed * gameTime.ElapsedGameTime.TotalSeconds);

                position += changeInPosition;
            }
        }

        public void Move(Direction direction)
        {
            movementList.Add(direction);
        }

        public void Stop(Direction direction)
        {
            movementList.Remove(direction);
        }

        public void UsePrimary()
        {
            if (stateMachine.CanAttack && selector.Primary != null && selector.Primary.Use(this))
            {
                stateMachine.ChangeState(PlayerState.Attacking);
            }
        }
        public void UseSecondary()
        {
            if (stateMachine.CanAttack && selector.Secondary != null && selector.Secondary.Use(this))
            {
                stateMachine.ChangeState(PlayerState.Attacking);
            }
        }
        public void UseItem(int location)
        {
            if (stateMachine.CanAttack && selector.Selectables.ContainsKey(location) && selector.Selectables[location] != null)
            {
                stateMachine.ChangeState(PlayerState.Attacking);
                selector.Selectables[location].Use(this);
            }

        }

        public void AddMaxHealth(int maxHealth)
        {
            this.maxHealth += maxHealth;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (animation == null)
            {
                // Don't draw the default sprite if player is dead.
                // Dying animation handled by #PlayAnimation.
                if (Health > 0)
                {
                    sprite.Draw(gameTime, spriteBatch);
                }
            }
            else
            {
                animation.Draw(gameTime, spriteBatch);
            }
        }

        public override ICollisionHandler GetCollisionHandler()
        {
            return collisionHandler;
        }

        public void Reset()
        {
            maxHealth = PlayerConfig.StartingHealth;
            Health = PlayerConfig.StartingHealth;
            inventory.Clear();
            stateMachine.ChangeState(PlayerState.Idle);
            invincibleTimer = 0; // Removing remaining invincibility time
        }

        public void PlayAnimation(IAnimatedSprite sprite, bool freezes)
        {
            if (freezes) 
            {
                stateMachine.ChangeState(PlayerState.Locked);
            }
            if (animation != null)
            {
                // Make sure to unsubscribe previous event.
                animation.ReachedEnd -= AnimationEnd;
            }
            animation = sprite;

            sprite.Play();
            sprite.ReachedEnd += AnimationEnd;
        }

        protected void AnimationEnd(object sender, EventArgs e)
        {
            animation = null;
            (sender as IAnimatedSprite).ReachedEnd -= AnimationEnd;
            stateMachine.ChangeState(PlayerState.Idle);
        }
    }
}
