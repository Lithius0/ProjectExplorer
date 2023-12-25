using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectExplorer.Character
{
    public class PlayerStateMachine
    {
        private PlayerState state = PlayerState.Idle;
        public PlayerState State => state;

        private double attackTimer = 0;

        public bool CanMove => state != PlayerState.Attacking && state != PlayerState.Locked;
        public bool CanAttack => state != PlayerState.Attacking && state != PlayerState.Locked;

        public event EventHandler<StateChangedArgs> StateChanged;

        /// <summary>
        /// Changes the state and sends out the StateChanged event.
        /// Does not have any validity checks.
        /// Also used to add additional behavior on a state change and helps with automatic state changes.
        /// </summary>
        protected void ChangeStateInternal(PlayerState newState)
        {
            // Recording the current state since the event needs the old state.
            StateChangedArgs args = new(state, newState);

            state = newState;
            switch(state)
            {
                case PlayerState.Attacking:
                    attackTimer = PlayerConfig.AttackDuration; 
                    break;
            }

            StateChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Attempts to change the state of the state machine.
        /// Will do checks to make sure the state change is valid.
        /// </summary>
        /// <param name="newState">The state to change to.</param>
        /// <returns>True if the state change was successful, false otherwise.</returns>
        public bool ChangeState(PlayerState newState)
        {
            if (newState == state) return false;

            bool doSwitch;
            switch (newState)
            {
                case PlayerState.Idle:
                    doSwitch = state != PlayerState.Attacking;
                    break;
                case PlayerState.Attacking:
                    doSwitch = CanAttack;
                    break;
                case PlayerState.Moving:
                    doSwitch = CanMove;
                    break;
                case PlayerState.Locked:
                    doSwitch = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (doSwitch)
            {
                ChangeStateInternal(newState);
            }

            return doSwitch;
        }

        public void Update(GameTime gameTime)
        {
            if (attackTimer <= 0 && state == PlayerState.Attacking)
            {
                attackTimer = 0;
                ChangeStateInternal(PlayerState.Idle);
            }
            else
            {
                attackTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }

    public class StateChangedArgs : EventArgs
    {
        public PlayerState OldState { get; }
        public PlayerState NewState { get; }

        public StateChangedArgs(PlayerState oldState, PlayerState newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
