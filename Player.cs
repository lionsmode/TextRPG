using System;
using Monsters;

namespace Characters
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; private set; } = 100;
        public int Damage { get; private set; } = 20;

        public bool IsAlive => Health > 0;

        public Player(string name)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Герой" : name;
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || !IsAlive)
                return;

            Health = Math.Min(100, Health + amount);
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - Math.Max(0, damage));
        }

        public void Attack(Monster monster)
        {
            if (monster != null && monster.IsAlive)
                monster.TakeDamage(Damage);
        }
    }
}