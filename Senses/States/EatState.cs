using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Senses.Components;
using Senses.Actors;

namespace Senses.States
{
    public class EatState : State
    {
        public FoodHunter hunter { get; set; }
        private StateManager state;


        public EatState(FoodHunter hunter, StateManager state)
        {
            this.hunter = hunter;
            this.state = state;
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            hunter.Draw(sb);
        }

        public void Enter()
        {

        }

        public void Leave()
        {

        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            if (hunter.isEnemyClose(state.enemy)) {
                state.ChangeState(new RunAwayState(hunter, state));
            }
            else if (hunter.isFoodClose(state.foods))
            {
                if (hunter.auxFood == null)
                {
                    hunter.sameDirectionIncreaseSpeed(hunter.locateFood(state.foods), state.foods);
                }
                else{
                    hunter.sameDirectionIncreaseSpeed(hunter.auxFood, state.foods);
                }
            }
            else {
                state.ChangeState(new SeekState(hunter, state));
            }
        }
    }
}
