using System.Collections;
using Script.FSM;
using Script.Util;
using UnityEngine;

namespace Script.Player
{
    public class Player_Attack : State<PlayerController>
    {
        private static readonly int s_Anim1Hash = Animator.StringToHash("Base Layer.Attack.Player_Attack_1");
        private static readonly int s_Anim2Hash = Animator.StringToHash("Base Layer.Attack.Player_Attack_2");
        private static readonly int s_Anim3Hash = Animator.StringToHash("Base Layer.Attack.Player_Attack_3");
        private static readonly int s_Attack = Animator.StringToHash("Attack");
        private WaitUntil m_WaitAttack1;
        private WaitUntil m_WaitAttack2;
        private WaitUntil m_WaitAttack3;
        private SpriteRenderer m_Sr;
        private readonly Vector2 m_Pivot = Vector2.one;
        private Coroutine m_Co;
        private bool m_CanAttack = true;

        protected override void Init()
        {
            m_Sr = owner.GetComponent<SpriteRenderer>();
            m_WaitAttack1 =
                new WaitUntil(() => machine.anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s_Anim1Hash);
            m_WaitAttack2 =
                new WaitUntil(() => machine.anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s_Anim2Hash);
            m_WaitAttack3 =
                new WaitUntil(() => machine.anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s_Anim3Hash);
        }

        public override void OnStateEnter()
        {
            machine.anim.SetTrigger(s_Attack);
            owner.StartCoroutine(machine.WaitIdle(typeof(Player_Movement), s_Anim1Hash));
            m_Co = owner.StartCoroutine(Attack());
        }

        public override void OnStateUpdate()
        {
            if (machine.anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s_Anim3Hash)
            {
                machine.anim.ResetTrigger(s_Attack);
            }
        }

        public override void OnStateChangePoint()
        {
            if (Input.GetMouseButtonDown(0) && m_CanAttack)
            {
                owner.StartCoroutine(AttackDelay());
                machine.anim.SetTrigger(s_Attack);
            }
        }

        private IEnumerator AttackDelay()
        {
            m_CanAttack = false;
            yield return new WaitForSeconds(0.5f);
            m_CanAttack = true;
        }

        public override void OnStateExit()
        {
            owner.StopCoroutine(m_Co);
            m_Co = null;
        }

        private void AttackCheck()
        {
            var _center =
                m_Sr.flipX
                    ? (Vector2) owner.transform.position + m_Pivot
                    : (Vector2) owner.transform.position - m_Pivot;
            var _col = Physics2D.OverlapBoxAll(_center, Vector2.one, 1f, 1 << 3);
            if (_col[0] != null)
            {
                foreach (var c in _col)
                {
                    if (c.TryGetComponent(out Enemy.Enemy e))
                    {
                        e.Hit(owner.Stat.Damage);
                    }
                }
            }
        }

        private IEnumerator Attack()
        {
            yield return m_WaitAttack1;
            AttackCheck();
            yield return m_WaitAttack2;
            AttackCheck();
            yield return m_WaitAttack3;
            AttackCheck();
        }
    }
}