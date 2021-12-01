using Script.FSM;
using UnityEngine;

namespace Script.Player
{
    public class Player_Movement : State<PlayerController>
    {
        private float m_Hor;
        private float m_Ver;

        public override void OnStateUpdate()
        {
            MovementInput();
            ActionInput();
        }

        private void MovementInput()
        {
            m_Hor = Input.GetAxisRaw("Horizontal");
            m_Ver = Input.GetAxisRaw("Vertical");

            var _dir = new Vector2(m_Hor, m_Ver).normalized;
            owner.transform.Translate(_dir * owner.Stat.moveSpeed * Time.deltaTime);
        }

        private void ActionInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 구르기
            }
        }
    }
}
