using Script.FSM;
using Script.Player;
using UnityEngine;

namespace Script.Enemy
{
    public class Enemy_Attack : State<Enemy>
    {
        public Enemy_Attack() : base("Base Layer.Enemy_Attack") => m_AttackHash = Animator.StringToHash("Attack");
        private readonly int m_AttackHash;
        private SpriteRenderer m_Sr;
        private readonly Vector2 m_Pivot = Vector2.one;

        protected override void Init()
        {
            m_Sr = owner.GetComponent<SpriteRenderer>();
        }

        public override void OnStateEnter()
        {
            machine.anim.SetTrigger(m_AttackHash);
            AttackCheck();
            owner.StartCoroutine(machine.WaitIdle(typeof(Enemy_Movement), animHash));
        }

        private void AttackCheck()
        {
            var _center =
                m_Sr.flipX
                    ? (Vector2) owner.transform.position + m_Pivot
                    : (Vector2) owner.transform.position - m_Pivot;
            var _col = Physics2D.OverlapBox(_center, Vector2.one, 1f, 1 << 8);
            if (_col != null)
            {
                if (_col.TryGetComponent(out PlayerController p))
                {
                    p.Hit(owner.m_Stat.damage);
                }
            }
        }
    }
}