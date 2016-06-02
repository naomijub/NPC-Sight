using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senses.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Senses.Actors;

namespace Senses.States
{
    public class StateManager
    {
        State state;
        bool hasCalledEnter = false;
        public Enemy enemy { get; set; }
        public IList<Food> foods { get; set; }

        public StateManager(FoodHunter hunter, Enemy enemy)
        {
            state = new SeekState(hunter, this);
            this.enemy = enemy;
            foods = new List<Food>();
        }

        public void Draw(SpriteBatch sb, GameTime gameTime)
        {
            state.Draw(sb, gameTime);
            enemy.Draw(sb);
            foreach (Food f in foods)
            {
                f.Draw(sb);
            }
        }

        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            inputHandler.Update();
            enemy.Update(gameTime, inputHandler);
            checkForFood(inputHandler);
            if (!hasCalledEnter)
            {
                this.state.Enter();
                hasCalledEnter = !hasCalledEnter;
            }

            state.Update(gameTime, inputHandler);
        }

        public void ChangeState(State state)
        {
            this.state.Leave();

            this.state = state;
            this.state.Enter();

        }

        public void checkForFood(InputHandler input)
        {
            Food aux = enemy.updateFood(input);
            if (aux != null)
            {
                foods.Add(aux);
            }
        }
    }
}
