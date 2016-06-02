using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Senses.Components
{
    public interface State
    {
        void Enter();
        void Leave();
        void Draw(SpriteBatch sb, GameTime gameTime);
        void Update(GameTime gameTime, InputHandler inputHandler);
    }
}
