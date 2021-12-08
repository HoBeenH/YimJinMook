using System.Collections;
using Script.FSM;
using Script.Manager;
using UnityEngine;
using static Script.Util.Facade;

namespace Script.Player
{
    public class Player_Movement : State<PlayerController>
    {
        private static readonly int s_IsMove = Animator.StringToHash("IsMove");
        private Rigidbody2D rig;
        private SpriteRenderer m_Sr;
        private bool canJump = true;
        private Task m_Task = Task.None;

        private IEnumerator JumpCoolDown()
        {
            canJump = false;
            yield return new WaitForSeconds(1f);
            canJump = true;
        }

        private void Jump()
        {
            if (canJump)
            {
                rig.AddForce(Vector2.up * 200, ForceMode2D.Force);
                owner.StartCoroutine(JumpCoolDown());
            }

            m_Task = Task.None;
        }

        protected override void Init()
        {
            rig = owner.GetComponent<Rigidbody2D>();
            m_Sr = owner.GetComponent<SpriteRenderer>();
        }

        public override void OnStateChangePoint()
        {
            ChangeState();
        }

        public override void OnStateUpdate()
        {
            MouseInput();
            MovementInput();
        }

        private void MovementInput()
        {
            var _hor = Input.GetAxisRaw("Horizontal");
            if (_hor != 0)
            {
                machine.anim.SetBool(s_IsMove, true);
                m_Sr.flipX = (_hor > 0);
                owner.transform.Translate(new Vector2(_hor, 0f) * owner.Stat.moveSpeed * Time.deltaTime);
            }
            else
            {
                machine.anim.SetBool(s_IsMove, false);
            }
        }


        public override void OnStateExit()
        {
            // m_Task = Task.None;
        }

        private void MouseInput()
        {
            Debug.Log("!!!!!!");
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Input");
                m_Task = _MouseManager.MouseAction();
            }
        }

        private void ChangeState()
        {
            if (m_Task == Task.None)
            {
                return;
            }
            switch (m_Task)
            {
                case Task.Jump:
                    Jump();
                    break;
                case Task.Attack:
                    machine.ChangeState(typeof(Player_Attack));
                    break;
                case Task.None:
                    break;
                default:
                    Debug.Log("Unknown Error");
                    break;
            }

            m_Task = Task.None;
        }
    }
}