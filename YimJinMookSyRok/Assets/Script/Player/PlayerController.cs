using System;
using Script.Data;
using Script.Default;
using Script.FSM;
using Script.Util;
using UnityEngine;
using static Script.Util.Facade;

public enum State
{
    None,
    Dodge,
    Defence
}
namespace Script.Player
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerController : MonoSingleton<PlayerController>
    {
        public PlayerStatus Stat { get; private set; }
        public State state;
        public Inventory Inventory { get; private set; }
        private StateMachine<PlayerController> m_Machine;
        private Animator m_Anim;

        #region Player Status

        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _moveSpeed;

        #endregion

        private void Awake() => Init();

        private void Init()
        {
            Inventory = new Inventory();
            Stat = new PlayerStatus(_health, _damage, _moveSpeed);
            m_Anim = GetComponent<Animator>();
            m_Machine = new StateMachine<PlayerController>(new Player_Movement(), this, m_Anim);
            m_Machine.AddState(new Player_Attack());
            m_Machine.AddState(new Player_Hit());
            m_Machine.AddState(new Player_Dodge());

            InitAction(ref _DataManager.save);
        }

        private void InitAction(ref Action<PlayData> data)
        {
            data += d =>
            {
                d.damage = Stat.damage;
                d.health = Stat.health;
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
                Hit(0);
            }
        }

        public void Hit(float damage)
        {
            switch (state)
            {
                case State.None:
                    m_Machine.ChangeState(typeof(Player_Hit));
                    break;
                case State.Dodge:
                    m_Machine.anim.SetTrigger("Hit");
                    break;
                case State.Defence:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // Stat.Health -= damage;
            // if (Stat.Health <= 0)
            // {
            //     // 사망
            // }
            // else
            
        }

        public void GetItem(ItemData item)
        {
            item.action?.Invoke(this);
            Inventory.GetItem(item);
        }
    }
}