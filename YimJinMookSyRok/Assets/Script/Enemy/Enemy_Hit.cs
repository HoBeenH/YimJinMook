using Script.FSM;
using UnityEngine;

namespace Script.Enemy
{
    public class Enemy_Hit : State<Enemy>
    {
        private readonly int m_HitHash;
        public Enemy_Hit() : base("Base Layer.Enemy_Hit") => m_HitHash = Animator.StringToHash("Hit");

        public override void OnStateEnter()
        {
            owner.StartCoroutine(machine.WaitIdle(typeof(Enemy_Movement), animHash));
            machine.anim.SetTrigger(m_HitHash);
        }
    }
}