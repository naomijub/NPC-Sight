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
    public abstract class Actors
    {
        protected Vector2 position;
        protected Vector2 instantDirection;

        public abstract void Draw(SpriteBatch sb);
        public abstract void Update(GameTime gameTime, InputHandler inputHandler);

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getInstDirection()
        {
            return instantDirection;
        }

        public void setPosition(Vector2 pos)
        {
            this.position = pos;
        }

        public void setInstDirection(Vector2 dir)
        {
            this.instantDirection = dir;
        }
    }
}
