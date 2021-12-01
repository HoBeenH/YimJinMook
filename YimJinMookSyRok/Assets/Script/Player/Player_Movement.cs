using Script.FSM;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Player
{
    public class Player_Movement : State<PlayerController>
    {
        private static readonly int s_Speed = Animator.StringToHash("Speed");
        private Rigidbody2D rig;
        private float m_Hor;
        private bool Flip => m_Hor > 0;
        private SpriteRenderer m_Sr;

        protected override void Init()
        {
            rig = owner.GetComponent<Rigidbody2D>();
            m_Sr = owner.GetComponent<SpriteRenderer>();
        }

        public override void OnStateUpdate()
        {
            MovementInput();
            ActionInput();
            Test();
        }

        private void MovementInput()
        {
            m_Hor = Input.GetAxisRaw("Horizontal");
            machine.anim.SetFloat(s_Speed,m_Hor);
            var _dir = new Vector2(m_Hor, 0f);
            m_Sr.flipX = Flip;
            owner.transform.Translate(_dir * owner.Stat.moveSpeed * Time.deltaTime);
        }

        private void ActionInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.AddForce(owner.transform.right * 30f * -1,ForceMode2D.Impulse);
            }
        }

        private void Test()
        {
            if (_MouseManager.Test)
            {
                rig.AddForce(Vector2.up * 100,ForceMode2D.Impulse);
            }
        }
    }
}
