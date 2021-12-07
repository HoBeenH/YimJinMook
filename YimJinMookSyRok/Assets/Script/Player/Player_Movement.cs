using System;
using Script.FSM;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Player
{
    public class Player_Movement : State<PlayerController>
    {
        private static readonly int s_IsMove = Animator.StringToHash("IsMove");
        private Rigidbody2D rig;
        private SpriteRenderer m_Sr;
        private Vector2 m_Dir;

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
            if (Input.GetKey(KeyCode.A))
            {
                m_Sr.sprite = owner.tmp;
                machine.anim.SetBool(s_IsMove, true);
                m_Dir = Vector2.left;
                m_Sr.flipX = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                machine.anim.SetBool(s_IsMove, true);
                m_Dir = Vector2.right;
                m_Sr.flipX = true;
            }
            else
            {
                machine.anim.SetBool(s_IsMove, false);
                m_Dir = Vector2.zero;
            }

            owner.transform.Translate(m_Dir * owner.Stat.moveSpeed * Time.deltaTime);
        }

        private void ActionInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rig.AddForce(owner.transform.right * 30f * -1, ForceMode2D.Impulse);
            }

            if (Input.GetMouseButtonDown(0) && owner.de)
            {
                machine.ChangeState(typeof(Player_Attack));
            }
        }

  

        private void Test()
        {
            if (_MouseManager.Test)
            {
                rig.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
            }
        }
    }
}