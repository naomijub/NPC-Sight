using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Senses.Components;
using Senses.Actors;

namespace Senses.States
{
    public class SeekState : State
    {
        public FoodHunter hunter { get; set; }
        private StateManager state;


        public SeekState(FoodHunter hunter, StateManager state)
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
            if (hunter.isEnemyClose(state.enemy) || hunter.near)
            {
                state.ChangeState(new RunAwayState(hunter, state));
            }
            else if (hunter.isFoodClose(state.foods))
            {
                hunter.setColor(Color.Green);
                state.ChangeState(new EatState(hunter, state));
            }
            else {
                hunter.setColor(Color.Red);
                hunter.dirManager();
                hunter.Update(gameTime, inputHandler);
            }

        }
    }
}
