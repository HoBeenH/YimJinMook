using System.Collections;
using Script.FSM;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Enemy
{
    public class Enemy_Movement : State<Enemy>
    {
        private static readonly int s_MoveHash = Animator.StringToHash("IsMove");
        private SpriteRenderer m_Sr;
        private readonly WaitForSeconds m_Delay = new WaitForSeconds(2.0f);
        private bool m_CanAttack = true;

        protected override void Init()
        {
            m_Sr = owner.GetComponent<SpriteRenderer>();
        }

        public override void OnStateUpdate()
        {
            var _dis = InPlayer();
            if (_dis <= 0.7f)
            {
                if (m_CanAttack)
                {
                    machine.ChangeState(typeof(Enemy_Attack));
                    owner.StartCoroutine(AttackDelay());
                }
                else
                {
                    machine.anim.SetBool(s_MoveHash, false);
                }
            }
            else if (_dis <= 5f)
            {
                var _dir = (_PlayerController.transform.position - owner.transform.position).normalized;
                machine.anim.SetBool(s_MoveHash, true);
                m_Sr.flipX = !(_dir.x <= 0);
                owner.transform.Translate(_dir * owner.m_Stat.moveSpeed * Time.deltaTime);
            }
            else
            {
                machine.anim.SetBool(s_MoveHash, false);
            }
        }

        public override void OnStateExit() => machine.anim.SetBool(s_MoveHash, false);

        private IEnumerator AttackDelay()
        {
            m_CanAttack = false;
            yield return m_Delay;
            m_CanAttack = true;
        }

        private float InPlayer() => (_PlayerController.transform.position - owner.transform.position).magnitude;
    }
}