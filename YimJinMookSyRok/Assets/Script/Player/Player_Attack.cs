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
        private int m_Anim;
        private const string ANIM_NAME = "Player_Attack_3";

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
            var _hit = Physics2D.OverlapBoxAll(owner.transform.position, Vector2.one, 0f, 1 << 3);
            if (_hit != null)
            {
                Debug.Log("!!!!");    
            }
        }
        
    }
}