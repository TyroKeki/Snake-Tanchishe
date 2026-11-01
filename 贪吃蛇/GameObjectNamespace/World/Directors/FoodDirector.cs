using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using 贪吃蛇.GameObjectNamespace.Block;
using 贪吃蛇.GameObjectNamespace.Entirety;
using 贪吃蛇.GameObjectNamespace.World.WorldComponents;
using 贪吃蛇.Main;
using 贪吃蛇.Toolkit;

namespace 贪吃蛇.GameObjectNamespace.World.Directors
{
    enum E_FoodRule
    {
        OneFood,
        MuchFood,
        NoFood,
    }
    internal class FoodDirector
    {
        private Random rand = new Random();
        // 很重要的两个信息，但是这么搞很蠢，万一以后有NPC蛇，可以用方法添加
        private WorldMap worldMap;
        private Snake snake;

        public E_FoodRule rule;
        private HashSet<Type> types = new HashSet<Type>();
        private List<Food> foods = new List<Food>();

        public FoodDirector(Snake snake, WorldMap map)
        {
            worldMap = map;
            this.snake = snake;
            types.Add(typeof(Food));
            types.Add(typeof(Wall));
            rule = E_FoodRule.MuchFood;
            Generate();
        }

        private void ToRendererSingle(Food food)
        {
            MapRenderer.Instance.PassInfo(food, food.pos.x, food.pos.y);
        }
        public void ToRenderer()
        {

        }

        public void RandomPos(Snake snake)
        {
            Vector2 tmpPos;
            while (true)
            {
                int x = rand.Next(1, Game.LogicMapWidth - 1);
                int y = rand.Next(1, Game.LogicMapHeight - 1);
                tmpPos = new Vector2(x, y);
                if (snake.CheckPositionOccupied(tmpPos))
                {
                    continue;
                }
                GameObject nowBlock = worldMap.NowMap[x, y];
                if(nowBlock != null)
                {
                    if (types.Contains(nowBlock.GetType()))
                    {
                        continue;
                    }
                }

                break;
            }
            Food newFood = new Food(tmpPos.x, tmpPos.y);
            foods.Add(newFood);
            worldMap.NowMap[tmpPos.x, tmpPos.y] = newFood;
        }

        public void RemoveFood(int x,int y)
        {
            if(worldMap.NowMap[x, y] is Food)
            {
                foods.Remove((Food)worldMap.NowMap[x, y]);
                worldMap.NowMap[x, y] = null;
            }
        }

        public bool CheckFood(Vector2 pos)
        {
            if(worldMap.NowMap[pos.x, pos.y] is Food)
            {
                return true;
            }
            return false;
        }

        public void Generate()
        {
            switch (rule)
            {
                case E_FoodRule.OneFood:
                    RandomPos(snake);
                    break;
                case E_FoodRule.MuchFood:
                    if(foods.Count == 0)
                    {
                        MuchGenerate(10);
                    }
                    else
                    {
                        RandomPos(snake);
                    }
                    break;
                case E_FoodRule.NoFood:
                    break;
                default:
                    break;
            }
        }

        public void MuchGenerate(int cnt)
        {
            for (int i = 0; i < cnt; i++)
            {
                RandomPos(snake);
            }
        }

    }
}
