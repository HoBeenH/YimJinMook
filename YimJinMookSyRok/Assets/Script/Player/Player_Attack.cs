using System.Collections;
using Script.FSM;
using Script.Util;
using UnityEngine;

namespace Script.Player
{
    public class Player_Attack : State<PlayerController>
    {
        private static readonly int s_Attack = Animator.StringToHash("Attack");
        private static readonly int s_AnimHash = Animator.StringToHash("Base Layer.Attack.Player_Attack_1");
        private BoxCollider2D m_Bc;
        private WaitUntil m_Until;
        private int m_Anim;
        private const string ANIM_NAME = "Player_Attack_3";

        protected override void Init()
        {
            m_Bc = owner.GetComponentsInChildren<BoxCollider2D>()[1];
            m_Bc.gameObject.SetActive(false);
            m_Until = new WaitUntil(() => machine.anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s_AnimHash);
        }

        public override void OnStateEnter()
        {
            machine.anim.SetTrigger(s_Attack);
            owner.StartCoroutine(machine.WaitIdle(typeof(Player_Movement), s_AnimHash));
            AttackCheck();
        }

        public override void OnStateUpdate()
        {
            if (machine.anim.GetCurrentAnimatorStateInfo(0).IsName(ANIM_NAME))
            {
                machine.anim.ResetTrigger(s_Attack);
            }
        }

        public override void OnStateChangePoint()
        {
            if (Input.GetMouseButtonDown(0))
            {
                machine.anim.SetTrigger(s_Attack);
            }
        }

        private void AttackCheck()
        {
            m_Bc.gameObject.SetActive(true);
        }
    }
}