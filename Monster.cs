using System;

namespace Monsters
{
    public class Monster
    {
        public string Name { get; set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public bool IsAlive => Health > 0;

        public Monster(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
        }

        public int Attack()
        {
            return IsAlive ? Damage : 0;
        }
    }
}