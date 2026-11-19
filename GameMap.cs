using System;
using System.Collections.Generic;
using Characters;
using Monsters;

namespace Game
{
    public class GameMap
    {
        public List<Location> Locations = new List<Location>
        {
            new Location(
                "Лесная поляна",
                "На поляне появился Полый. Победите его, чтобы пройти дальше.",
                new Monster("Полый", 50, 10),
                new int[,]
                {
                    { 1, 1, 1 },
                    { 1, 0, 2 },
                    { 1, 1, 3 }
                }),

            new Location(
                "Тёмный лес",
                "В лесу обитает Скелет. Будьте осторожны.",
                new Monster("Скелет", 70, 20),
                new int[,]
                {
                    { 1, 2, 1 },
                    { 1, 0, 1 },
                    { 1, 1, 3 }
                }),

            new Location(
                "Заброшенная крепость",
                "Здесь находится Повелитель — последний противник.",
                new Monster("Повелитель", 100, 15),
                new int[,]
                {
                    { 1, 1, 1 },
                    { 2, 0, 1 },
                    { 1, 1, 3 }
                })
        };

        public int Index = 0;

        public event Action LocationChanged;

        public Location Current => Locations[Index];

        public bool IsLastLocation =>
            Index == Locations.Count - 1;

        public bool Next()
        {
            if (IsLastLocation)
                return false;

            Index++;
            LocationChanged?.Invoke();
            return true;
        }

        public void Draw(Player hero, int heroX, int heroY)
        {
            Location location = Current;

            Console.WriteLine();
            Console.WriteLine(
                $"Локация {Index + 1}: {location.Name}");

            Console.WriteLine(location.Description);

            Console.WriteLine(
                "0 — герой, 1 — путь, 2 — враг, 3 — выход, # — стена");

            Console.WriteLine();

            for (int y = 0; y < location.Map.GetLength(0); y++)
            {
                for (int x = 0; x < location.Map.GetLength(1); x++)
                {
                    if (x == heroX && y == heroY)
                    {
                        Console.Write("0 ");
                    }
                    else
                    {
                        int cell = location.Map[y, x];

                        if (cell == 2 && location.EnemyDefeated)
                            cell = 1;

                        Console.Write((cell == 0 ? "#" : cell.ToString()) + " ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine(
                $"\n{hero.Name} — HP: {hero.Health}, урон: {hero.Damage}");
        }
    }
}