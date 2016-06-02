using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Senses.Components
{
    public class BackgroundBuilder
    {
        public Texture2D background { get; set; }
        public Texture2D desertBg { get; set; }
        private Rectangle rect;

        public BackgroundBuilder(Texture2D bg, Texture2D desertBg)
        {
            this.background = bg;
            this.desertBg = desertBg;
        }

        public void Draw(SpriteBatch sb)
        { 
            sb.Draw(desertBg, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
            for (int y = 0; y < 31; y++)
            {
                for (int x = 0; x < 41; x++)
                {
                    if (((x * y) % 15) == 0) {
                        rect = new Rectangle(0, 128, 16, 16);
                        sb.Draw(background, new Vector2(x * 20, y * 20), rect, Color.White );
                    }
                    if (((x * y) % 45) == 0)
                    {
                        rect = new Rectangle(64, 144, 16, 16);
                        sb.Draw(background, new Vector2(x * 20, y * 20), rect, Color.White);
                    }
                    if (((x * y) % 221) == 0)
                    {
                        rect = new Rectangle(32, 80, 48, 16);
                        sb.Draw(background, new Vector2(x * 20, y * 20), rect, Color.White);
                    }
                }
            }
            
        }
    }
}
