using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Senses.Actors
{
    public class Enemy : Actors
    {
        public Texture2D enemyTex { get; set; }
        public Texture2D food { get; set; }
        Rectangle map;

        public Enemy(Texture2D enemyTex, Texture2D food)
        {
            this.enemyTex = enemyTex;
            this.food = food;
            map = new Rectangle(16, 16, 768, 568);
            position.X = 32;
            position.Y = 32;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(enemyTex, position, null, null, new Vector2(enemyTex.Width, enemyTex.Height) / 2, 0.0f,
                null, Color.White, SpriteEffects.None, 0.0f);
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            updateInstDirection(inputHandler);
        }

        public void updateInstDirection(InputHandler inputHandler)
        {

            if (inputHandler.KeyDown(Keys.Up))
            {
                instantDirection.Y = -2.0f;
            }
            else if (inputHandler.KeyDown(Keys.Down))
            {
                instantDirection.Y = +2.0f;
            }
            else if (inputHandler.KeyDown(Keys.Right))
            {
                instantDirection.X = 2.0f;
            }
            else if (inputHandler.KeyDown(Keys.Left))
            {
                instantDirection.X = -2.0f;
            }
            else {
                instantDirection.X = 0;
                instantDirection.Y = 0;
            }
            if (insideRect())
            {
                position += instantDirection;
            }
        }

        public bool insideRect()
        {
            int auxX = (int)(position.X + instantDirection.X);
            int auxY = (int)(position.Y + instantDirection.Y);
            return map.Contains(auxX, auxY);
        }

        public Food updateFood(InputHandler inputHandler)
        {
            if (inputHandler.KeyPressed(Keys.Space))
            {
                Food food = new Food(this.food, this.position);
                return food;
            }
            return null;
        }
    }
}
