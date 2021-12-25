using System.Collections;
using Script.FSM;
using UnityEngine;

namespace Script.Player
{
    public class Player_Defence : State<PlayerController>
    {
        private static readonly int s_DefAnim = Animator.StringToHash("Defence");

        public override void OnStateEnter()
        {
            owner.E_State = State.Defence;
            machine.anim.SetBool(s_DefAnim, true);
            owner.Stat.recoveryStamina -= 0.5f;
        }

        public override void OnStateUpdate()
        {
            if (Input.GetMouseButtonUp(1))
            {
                machine.ChangeState(typeof(Player_Movement));
            }

        }

        public override void OnStateExit()
        {
            machine.anim.SetBool(s_DefAnim, false);
            owner.E_State = State.None;
            owner.Stat.recoveryStamina += 0.5f;
        }
    }
}
