using System;
using Script.Data;
using Script.Default;
using Script.FSM;
using Script.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Serialization;
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
        public State E_State;

        public Inventory Inventory { get; private set; }
        private StateMachine<PlayerController> m_Machine;

        #region Player Status

        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _stamina;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _recoveryStamina;

        #endregion

        private void Awake() => Init();

        private void Init()
        {
            Inventory = new Inventory();
            Stat = new PlayerStatus(_health, _damage, _moveSpeed, _stamina, _recoveryStamina);
            m_Machine = new StateMachine<PlayerController>(new Player_Movement(), this, GetComponent<Animator>(),
                "Base Layer.Player_Idle");
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
                d.damage = Stat.Damage;
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

            Stat.Stamina += Stat.recoveryStamina;
        }

        public void Hit(float damage)
        {
            m_Machine.anim.SetTrigger("Hit");
            if (E_State == State.Dodge || E_State == State.Defence)
            {
                return;
            }

            Stat.Health -= damage;
            m_Machine.ChangeState(typeof(Player_Hit));
            if (Stat.Health <= 0)
            {
                // 사망
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
                E_State = State.Ex;
            }
        }
    }
}