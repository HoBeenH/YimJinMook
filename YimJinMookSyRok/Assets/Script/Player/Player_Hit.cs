using Script.FSM;
using UnityEngine;

namespace Script.Player
{
    public class Player_Hit : State<PlayerController>
    {
        private static int Hit;

        public Player_Hit() : base("Base Layer.Player_Hit") => Hit = Animator.StringToHash("Hit");

        public override void OnStateEnter()
        {
            machine.anim.SetTrigger(Hit);
            owner.StartCoroutine(machine.WaitIdle(typeof(Player_Movement), animHash));
        }
    }
}
