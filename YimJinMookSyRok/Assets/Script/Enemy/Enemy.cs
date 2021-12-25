using System;
using System.Collections;
using Script.Default;
using Script.FSM;
using UnityEngine;

namespace Script.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private StateMachine<Enemy> m_Machine;
        private Animator m_Anim;
        private SpriteRenderer m_Sr;
        [HideInInspector]public EnemyStatus m_Stat;

        #region Stat
        [SerializeField] private float m_Health;
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_MoveSpeed;
        #endregion

        private void Awake()
        {
            m_Stat = new EnemyStatus(m_Health, m_Damage, m_MoveSpeed);
            m_Machine = new StateMachine<Enemy>(new Enemy_Movement(), this, GetComponent<Animator>(),"Base Layer.Enemy_Idle");
            m_Machine.AddState(new Enemy_Attack());
            m_Machine.AddState(new Enemy_Hit());
        }

        private void Update() => m_Machine?.OnUpdate();

        public void Hit(float damage)
        {
            m_Stat.Health -= damage;
            m_Machine.ChangeState(typeof(Enemy_Hit));
            if (m_Stat.Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}