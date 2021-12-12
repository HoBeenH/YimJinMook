using Script.Util;
using UnityEngine;
using UnityEngine.UI;
using static Script.Util.Facade;

namespace Script.Manager
{
    public class UiManager : MonoSingleton<UiManager>
    {
        #region Mouse

        private Texture2D m_DefaultCursor;
        private Texture2D m_AttackCursor;
        private Texture2D m_JumpCursor;

        private Task CurrentCursor
        {
            set
            {
                switch (value)
                {
                    case Task.Jump:
                        Cursor.SetCursor(m_JumpCursor,Vector2.zero,CursorMode.Auto);
                        break;
                    case Task.Attack:
                        Cursor.SetCursor(m_AttackCursor,Vector2.zero,CursorMode.Auto);
                        break;
                    default:
                        Cursor.SetCursor(m_DefaultCursor,Vector2.zero,CursorMode.Auto);
                        break;
                }
            }
        }

        #endregion
        public Image healthUI;
        public Image staminaUI;

        private void Awake()
        {
            m_DefaultCursor = Resources.Load<Texture2D>("Cursor/Default");
            m_AttackCursor = Resources.Load<Texture2D>("Cursor/Attack");
            m_JumpCursor = Resources.Load<Texture2D>("Cursor/Jump");
        }

        public void ChangeHealthValue(float maxHealth, float currentHealth)
        {
            healthUI.fillAmount = maxHealth / currentHealth;
        }
        
        private void Update() => CurrentCursor = _Input_Manager.task;

    }
}