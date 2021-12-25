using System;
using static Script.Util.Facade;

namespace Script.Default
{
    public class Status
    {
        protected float health;
        public float damage;
        public float moveSpeed;
    }

    public class PlayerStatus : Status
    {
        public float maxHealth;
        private float stamina;
        public float maxStamina;
        public float recoveryStamina;

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
                else if (stamina < 0)
                {
                    stamina = 0;
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

        public PlayerStatus(float health, float damage, float moveSpeed, float stamina, float recoveryStamina)
        {
            this.health = health;
            this.maxHealth = health;
            this.stamina = stamina;
            this.maxStamina = stamina;
            this.damage = damage;
            this.moveSpeed = moveSpeed;
            this.recoveryStamina = recoveryStamina;
        }
    }

    public class EnemyStatus : Status
    {
        public float Health
        {
            get => base.health;
            set => base.health = value;
        }

        public EnemyStatus(float health, float damage, float moveSpeed)
        {
            base.health = health;
            base.damage = damage;
            base.moveSpeed = moveSpeed;
        }
    }
}