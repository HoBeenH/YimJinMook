using System;
using static Script.Util.Facade;

namespace Script.Default
{
    public class Status
    {
        public float health;
        public float damage;
        public float moveSpeed;
    }

    public class PlayerStatus : Status
    {
        public float maxHealth;
        public float stamina;
        public float maxStamina;

        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }

                _UiManager.ChangeHealthValue(maxHealth, health);
            }
        }

        public float Stamina
        {
            get => stamina;
            set
            {
                stamina = value;
                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
            }
        }

        public float Damage
        {
            get => this.damage;
            set
            {
                damage = value;
                if (damage < 0)
                {
                    damage = 0.1f;
                }
            }
        }

        public float MoveSpeed
        {
            get => this.moveSpeed;
            set
            {
                moveSpeed = value;
                if (moveSpeed < 0)
                {
                    moveSpeed = 0.5f;
                }
            }
        }

        public PlayerStatus(float health, float damage, float moveSpeed)
        {
            this.health = health;
            this.maxHealth = health;
            this.damage = damage;
            this.moveSpeed = moveSpeed;
        }
    }
}