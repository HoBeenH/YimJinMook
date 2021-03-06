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
        private Task m_Task = Task.None;
        private bool m_NowJump = false;
        
        private void Jump()
        {
            if (!m_NowJump)
            {
                m_NowJump = true;
                rig.AddForce(Vector2.up * 200, ForceMode2D.Force);
            }

            m_Task = Task.None;
        }

        private void IsGround()
        {
            if (rig.velocity.y < 0)
            {
                var _ray = Physics2D.Raycast(owner.transform.position, Vector2.down, 1, 1 << 7);
                if (_ray.collider != null)
                {
                    m_NowJump = false;
                }
            }
        }

        protected override void Init()
        {
            rig = owner.GetComponent<Rigidbody2D>();
            m_Sr = owner.GetComponent<SpriteRenderer>();
            
            EventSystem.BindEvent(Task.Attack, sender => machine.ChangeState(typeof(Player_Attack)));
            EventSystem.BindEvent(Task.Dodge, sender => machine.ChangeState(typeof(Player_Dodge)));
            EventSystem.BindEvent(Task.Defence, sender => machine.ChangeState(typeof(Player_Defence)));
            EventSystem.BindEvent(Task.Jump, sender => Jump());
        }

        public override void OnStateUpdate()
        {
            InputValue();
            IsGround();
            MovementInput();
        }

        public override void OnStateExit()
        {
            machine.anim.SetBool(s_IsMove, false);
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

        private void InputValue()
        {
            if (Input.GetMouseButtonDown(0))
                EventSystem.CallEvent(_Input_Manager.MouseAction());
            else if (Input.GetKeyDown(KeyCode.Space) && machine.anim.GetBool(s_IsMove))
                EventSystem.CallEvent(Task.Dodge);
            else if (Input.GetKeyDown(KeyCode.Space))
                EventSystem.CallEvent(Task.Defence);
        }
    }
}