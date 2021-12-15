using System;
using Script.Data;
using Script.Default;
using Script.FSM;
using Script.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using static Script.Util.Facade;

public enum State
{
    None,
    Dodge,
    Defence,
    Ex
}

namespace Script.Player
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerController : MonoSingleton<PlayerController>
    {
        public PlayerStatus Stat { get; private set; }
        private State E_State;

        public State EState
        {
            get => E_State;
            set
            {
                E_State = value;
                switch (value)
                {
                    case State.None:
                        break;
                    case State.Dodge:
                        break;
                    case State.Defence:
                        break;
                    case State.Ex:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public Inventory Inventory { get; private set; }
        private StateMachine<PlayerController> m_Machine;
        private Animator m_Anim;

        #region Player Status

        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _stamina;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _minStamina;

        #endregion

        private void Awake() => Init();

        private void Init()
        {
            Inventory = new Inventory();
            Stat = new PlayerStatus(_health, _damage, _moveSpeed, _stamina, _minStamina);
            m_Anim = GetComponent<Animator>();
            m_Machine = new StateMachine<PlayerController>(new Player_Movement(), this, m_Anim);
            m_Machine.AddState(new Player_Attack());
            m_Machine.AddState(new Player_Hit());
            m_Machine.AddState(new Player_Defence());
            m_Machine.AddState(new Player_Dodge());

            InitAction(ref _DataManager.save);
        }

        private void InitAction(ref Action<PlayData> data)
        {
            data += d =>
            {
                d.damage = Stat.damage;
                d.health = Stat.Health;
                d.maxHealth = Stat.maxHealth;
                d.moveSpeed = Stat.moveSpeed;
            };
            // data += Inventory.SaveData;
        }

        private void Update()
        {
            m_Machine?.OnUpdate();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Hit(10);
            }
        }

        public void Hit(float damage)
        {
            Stat.Health -= damage;
            Debug.Log(Stat.Health);
            if (Stat.Health <= 0)
            {
                // 사망
            }

            m_Machine.anim.SetTrigger("Hit");
            if (EState == State.None)
            {
                m_Machine.ChangeState(typeof(Player_Hit));
            }
        }

        public void GetItem(ItemData item)
        {
            item.action?.Invoke(this);
            Inventory.GetItem(item);
        }


        private void StaminaChange(float value)
        {
            Stat.Stamina -= value;
            if (Stat.Stamina >= 0)
            {
                EState = State.Ex;
            }
        }
    }
}