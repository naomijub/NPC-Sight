using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Senses.Actors
{
    public class Food : Actors
    {
        public Texture2D food { get; set; }

        public Food(Texture2D food, Vector2 pos)
        {
            this.food = food;
            this.position = pos;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(food, position, null, null, new Vector2(food.Width, food.Height) / 2, 0.0f,
                null, Color.White, SpriteEffects.None, 0.0f);
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {

        }
    }
}
