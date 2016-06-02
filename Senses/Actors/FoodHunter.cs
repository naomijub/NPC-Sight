using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Senses.Actors;

namespace Senses.Actors
{
    public class FoodHunter : Actors
    {
        Random rg;
        Rectangle map;
        public Texture2D foodHunter { get; set; }
        public Texture2D seeRadius { get; set; }
        public Food auxFood { get; set; }
        private Color color;
        public bool near { get; set; }

        public FoodHunter(Texture2D hunter, Texture2D radius) : base()
        {
            rg = new Random();
            map = new Rectangle(20, 20, 750, 545);
            position.X = 100 + rg.Next(50, 401);
            position.Y = 100 + rg.Next(50, 401);
            this.foodHunter = hunter;
            this.seeRadius = radius;

            instantDirection.Y = rg.Next(0, 9) - 4;
            instantDirection.X = rg.Next(0, 9) - 4;

            near = false;
            color = Color.Red;

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(seeRadius, position, null, null, new Vector2(seeRadius.Width, seeRadius.Height) / 2, 0.0f,
                new Vector2(3, 3), color, SpriteEffects.None, 0.0f);
            sb.Draw(foodHunter, position, null, null, new Vector2(foodHunter.Width, foodHunter.Height) / 2, 0.0f,
                null, Color.White, SpriteEffects.None, 0.0f);
        }

        public override void Update(GameTime gameTime, InputHandler inputHandler)
        {
            position += instantDirection;

        }

        public bool insideRect(Vector2 position)
        {
            return map.Contains((int)position.X, (int)position.Y);
        }

        private void changeDir(Vector2 dir)
        {
            if(map.Top >= position.Y - (foodHunter.Height / 2) + instantDirection.Y)
            {
                instantDirection.Y = - dir.Y;
                instantDirection.X = getNewFloat();

            }
            if (map.Bottom <= position.Y + (foodHunter.Height / 2) + instantDirection.Y)
            {
                instantDirection.Y = -dir.Y;
                instantDirection.X = getNewFloat();
            }
            if (map.Right <= position.X + (foodHunter.Width / 2) + instantDirection.X)
            {
                instantDirection.X = -dir.X;
                instantDirection.Y = getNewFloat();
            }
            if (map.Left >= position.X - (foodHunter.Width / 2) + instantDirection.X)
            {
                instantDirection.X = -dir.X;
                instantDirection.Y = getNewFloat();
            }

        }

        public float getNewFloat() {
            return ((rg.Next(0, 2) * 2) - 1) * (rg.Next(5, 16) / 5);
        }

        public void dirManager()
        {
            if (!insideRect(position))
            {
                changeDir(instantDirection);
            }
        }

        public float getDistance(Actors actor) {
            return (float)Math.Sqrt(Math.Pow((position.X - actor.getPosition().X), 2)
                + Math.Pow((position.Y - actor.getPosition().Y), 2));
        }

        public void opositeDirectionIncreaseSpeed(Enemy enemy)
        {
            float distance = getDistance(enemy);
            near = true;
            if (near)
            {
                float auxInstDirX = determinateInstDir(position.X, enemy.getPosition().X, distance);
                float auxInstDirY = determinateInstDir(position.Y, enemy.getPosition().Y, distance);
                Vector2 instDirVector = new Vector2(auxInstDirX, auxInstDirY);

                //regular vector
                if (insideRect(position + instDirVector))
                {
                    instantDirection = instDirVector;
                    position += instantDirection;
                }
                //escaping Y vector
                else if (insideRect(position + new Vector2(auxInstDirX, -auxInstDirY)))
                {
                    instantDirection = new Vector2(auxInstDirX, -auxInstDirY);
                    position += instantDirection;
                }
                //escaping x vector
                else if (insideRect(position + new Vector2(-auxInstDirX, auxInstDirY)))
                {
                    instantDirection = new Vector2(-auxInstDirX, auxInstDirY);
                    position += instantDirection;
                }
            }
            if (distance > 250) { near = false; }
        }

        public float determinateInstDir(float pos, float enemyPos, float dist)
        {
            float aux = pos - enemyPos;
            return 2.3f * aux / dist;
        }

        public bool isEnemyClose(Enemy enemy)
        {
            float distance = getDistance(enemy);
            return (distance <= ((enemy.enemyTex.Width / 2) + (seeRadius.Width * 3 / 2) + 1) ? true : false);
        }

        public bool isFoodClose(IList<Food> foods)
        {
            foreach (Food f in foods)
            {
                float distance = getDistance(f);
                if (distance <= ((f.food.Width / 2) + (seeRadius.Width * 3 / 2)))
                {
                    return true;
                }
            }
            return false;
        }

        public Food locateFood(IList<Food> foods)
        {
            float distanteClosest = 10000.0f;
            for (int i = 0; i < foods.Count; i++)
            {
                float distance = getDistance(foods[i]);
                if (distance <= ((foods[i].food.Width / 2) + (seeRadius.Width * 3 / 2)) && distanteClosest > distance)
                {
                    distanteClosest = distance;
                    auxFood = foods[i];
                }
            }
            return auxFood;
        }

        public void sameDirectionIncreaseSpeed(Food food, IList<Food> foods)
        {

                float distance = getDistance(food);

                float auxInstDirX = determinateInstDir(position.X, food.getPosition().X, distance);
                float auxInstDirY = determinateInstDir(position.Y, food.getPosition().Y, distance);
                Vector2 instDirVector = new Vector2(auxInstDirX, auxInstDirY);

                //regular vector
                if (insideRect(position + instDirVector))
                {
                    position -= instDirVector;
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

            if (auxFood != null && eat(auxFood, (float)distance))
            {
                foods.Remove(auxFood);
                auxFood = null;
            }
        }

        public bool eat(Food food, float dist)
        {
            if (dist < (food.food.Width / 2 + foodHunter.Width / 2))
            {
                return true;
            }
            return false;
        }

        public void setColor(Color color) {
            this.color = color;
        }
    }
}
