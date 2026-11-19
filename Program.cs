using System;
using Characters;
using Monsters;
using Game;
using Logging;

class Program
{
    static void Main()
    {
        Console.Write("Имя героя: ");
        Player hero = new Player(Console.ReadLine());

        GameMap map = new GameMap();
        int heroX = 0, heroY = 0;
        using GameLogger logger = new GameLogger();
        logger.Log($"Начало игры. Герой: {hero.Name}");

        while (hero.IsAlive)
        {
            map.Draw(hero, heroX, heroY);

            Console.WriteLine("W - вверх, A - влево");
            Console.WriteLine("S - вниз, D - вправо");
            Console.WriteLine("H - лечение, Q - выход");
            Console.Write("Выбор: ");

            string key = (Console.ReadLine() ?? "").ToUpper();
            logger.Log($"Команда: {key}");

            if (key == "Q")
            {
                logger.Log("Игрок вышел из игры");
                break;
            }

            if (key == "H")
            {
                int before = hero.Health;
                hero.Heal(20);
                logger.Log($"Лечение: {before} -> {hero.Health} HP");
                continue;
            }

            int x = heroX;
            int y = heroY;

            if (key == "W") y--;
            if (key == "S") y++;
            if (key == "A") x--;
            if (key == "D") x++;

            Location location = map.Current;

            if (x < 0 || x >= location.Map.GetLength(1) ||
                y < 0 || y >= location.Map.GetLength(0))
            {
                Console.WriteLine("Туда нельзя!");
                logger.Log("Попытка выйти за пределы карты");
                continue;
            }

            int cell = location.Map[y, x];

            if (cell == 2 && !location.EnemyDefeated)
            {
                Monster enemy = location.Enemy;
                logger.Log($"Начало боя: {enemy.Name}");

                while (hero.IsAlive && enemy.IsAlive)
                {
                    Console.WriteLine(
                        $"{hero.Name}: {hero.Health} HP");
                    Console.WriteLine(
                        $"{enemy.Name}: {enemy.Health} HP");

                    Console.WriteLine("Enter - атаковать");
                    Console.ReadLine();

                    hero.Attack(enemy);
                    logger.Log($"{hero.Name} атакует {enemy.Name}. HP врага: {enemy.Health}");

                    if (enemy.IsAlive)
                    {
                        int damage = enemy.Attack();
                        hero.TakeDamage(damage);
                        logger.Log($"{enemy.Name} наносит {damage} урона. HP героя: {hero.Health}");
                    }
                }

                if (!enemy.IsAlive)
                {
                    Console.WriteLine("Враг побеждён!");
                    location.EnemyDefeated = true;
                    logger.Log($"Победа над {enemy.Name}");
                    heroX = x;
                    heroY = y;
                }
            }
            else if (cell == 3)
            {
                if (location.EnemyDefeated)
                {
                    if (map.Next())
                    {
                        heroX = 0;
                        heroY = 0;
                        logger.Log($"Переход в локацию: {map.Current.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Ты прошёл игру!");
                        logger.Log("Игра пройдена");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Сначала победи врага!");
                    logger.Log("Выход закрыт: враг ещё жив");
                }
            }
            else if (cell == 1)
            {
                heroX = x;
                heroY = y;
            }
            if (heroX == x && heroY == y)
                logger.Log($"Позиция: X={heroX}, Y={heroY}, локация: {map.Current.Name}");
        }

        logger.Log(hero.IsAlive ? "Игра окончена" : "Герой погиб");
        Console.WriteLine("Игра окончена.");
    }
}