using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Senses.Actors
{
    public class FoodHunter : Actors
    {
        Random rg;
        Rectangle map;
        public Texture2D foodHunter { get; set; }
        public Texture2D seeRadius { get; set; }
        private static int count = 1;
        private Food food;

        public FoodHunter(Texture2D hunter, Texture2D radius) : base() {
            rg = new Random();
            map = new Rectangle(20, 20, 750, 550);
            position.X = 500;
            position.Y = 500;
            this.foodHunter = hunter;
            this.seeRadius = radius;

            instantDirection.Y = rg.Next(0, 9) - 4;
            instantDirection.X = rg.Next(0, 9) - 4;

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(seeRadius, position, null, null, new Vector2(seeRadius.Width, seeRadius.Height)/2, 0.0f, 
                new Vector2(3, 3), Color.White, SpriteEffects.None, 0.0f);
            sb.Draw(foodHunter, position, null, null, new Vector2(foodHunter.Width, foodHunter.Height)/2, 0.0f, 
                null, Color.White, SpriteEffects.None, 0.0f);
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
                position += instantDirection;
            
        }

        public bool insideRect(Vector2 position) {
            return map.Contains((int)position.X, (int)position.Y);
        }

        private void changeDir(Vector2 dir) {
            if (dir.X == 0.0f)
            {
                count *= -1;
                instantDirection.X = count * 1.1f;
            }
            else {
                instantDirection.X = -(rg.Next(1, 11) / 5) * dir.X;
            }

            if (dir.Y == 0.0f)
            {
                count *= -1;
                instantDirection.Y = count * 1.1f;
            }
            else {
                instantDirection.Y = -(rg.Next(1, 11) / 5) * dir.Y;
            }
        }

        public void dirManager() {
            if (!insideRect(position))
            {
                changeDir(instantDirection);
            }
        }

        public void opositeDirectionIncreaseSpeed(Enemy enemy) {
            float distance = (float)Math.Sqrt(Math.Pow((position.X - enemy.getPosition().X), 2)
                + Math.Pow((position.Y - enemy.getPosition().Y), 2));
            float auxInstDirX = determinateInstDirEnemy(position.X, enemy.getPosition().X, distance);
            float auxInstDirY = determinateInstDirEnemy(position.Y, enemy.getPosition().Y, distance);
            Vector2 instDirVector = new Vector2(auxInstDirX, auxInstDirY);

            //regular vector
            if (insideRect(position + instDirVector))
            {
                position += instDirVector;
            }
            //escaping Y vector
            else if (insideRect(position + new Vector2(auxInstDirX, -auxInstDirY)))
            {
                position += new Vector2(auxInstDirX, -auxInstDirY);
            }
            //escaping x vector
            else if (insideRect(position + new Vector2(-auxInstDirX, auxInstDirY))) {
                position += new Vector2(-auxInstDirX, auxInstDirY);
            }
            
        }

        public float determinateInstDirEnemy(float pos, float enemyPos, float dist) {
            float aux = pos - enemyPos;
            return 2.5f * aux / dist;
        }

        public bool isEnemyClose(Enemy enemy) {
            double distance = Math.Sqrt(Math.Pow((position.X - enemy.getPosition().X), 2) 
                + Math.Pow((position.Y - enemy.getPosition().Y), 2));
            return (distance <= ((enemy.enemyTex.Width / 2) + (seeRadius.Width * 3 / 2)) ? true : false); 
        }

        public bool isFoodClose(IList<Food> foods) {
            foreach (Food f in foods) {
                double distance = Math.Sqrt(Math.Pow((position.X - f.getPosition().X), 2)
                + Math.Pow((position.Y - f.getPosition().Y), 2));
                if (distance <= ((f.food.Width / 2) + (seeRadius.Width * 3 / 2))){
                    return true;
                }
            }
            return false;
        }

        public Food locateFood(IList<Food> foods)
        {
            for(int i = 0; i < foods.Count;i++)
            {
                double distance = Math.Sqrt(Math.Pow((position.X - foods[i].getPosition().X), 2)
                + Math.Pow((position.Y - foods[i].getPosition().Y), 2));
                if (distance <= ((foods[i].food.Width / 2) + (seeRadius.Width * 3 / 2)))
                {
                    if (eat(foods[i], (float)distance)) {
                        foods.Remove(foods[i]);
                    }
                    return foods[i];
                }
            }
            return null;
        }

        public void sameDirectionIncreaseSpeed(Food food) {
            if (food != null)
            {
                float distance = (float)Math.Sqrt(Math.Pow((position.X - food.getPosition().X), 2)
                    + Math.Pow((position.Y - food.getPosition().Y), 2));

                float auxInstDirX = determinateInstDirEnemy(position.X, food.getPosition().X, distance);
                float auxInstDirY = determinateInstDirEnemy(position.Y, food.getPosition().Y, distance);
                Vector2 instDirVector = new Vector2(auxInstDirX, auxInstDirY);

                //regular vector
                if (insideRect(position + instDirVector))
                {
                    position += instDirVector;
                }
                //escaping Y vector
                else if (insideRect(position + new Vector2(auxInstDirX, -auxInstDirY)))
                {
                    position += new Vector2(auxInstDirX, -auxInstDirY);
                }
                //escaping x vector
                else if (insideRect(position + new Vector2(-auxInstDirX, auxInstDirY)))
                {
                    position += new Vector2(-auxInstDirX, auxInstDirY);
                }
            }
        }

        public float determinateInstDirFood(float pos, float foodPos, float dist)
        {
            float aux = foodPos - pos;
            return 2.0f * aux / dist;
        }

        public bool eat(Food food, float dist) {
            if (dist < (food.food.Width / 2 + foodHunter.Width / 2)) {
                return true;
            }
            return false;
        }
    }
}
