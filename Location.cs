using Monsters;

namespace Game
{
    public class Location
    {
        public string Name;
        public string Description;
        public Monster Enemy;
        public int[,] Map;

        public bool EnemyDefeated;

        public Location(
            string name,
            string description,
            Monster enemy,
            int[,] map)
        {
            Name = name;
            Description = description;
            Enemy = enemy;
            Map = map;
            EnemyDefeated = false;
        }
    }
}