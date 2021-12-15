using System.Collections;
using Script.FSM;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Player
{
    public class Player_Defence : State<PlayerController>
    {
        private static readonly int s_DefAnim = Animator.StringToHash("Defence");

        public override void OnStateEnter()
        {
            owner.EState = State.Defence;
            machine.anim.SetBool(s_DefAnim, true);
        }

        public override void OnStateUpdate()
        {
            if (Input.GetMouseButtonUp(1))
            {
                machine.ChangeState(typeof(Player_Movement));
            }

            owner.Stat.Stamina -= 0.05f;
        }

        public override void OnStateExit()
        {
            machine.anim.SetBool(s_DefAnim, false);
            owner.EState = State.None;
        }
    }
}
