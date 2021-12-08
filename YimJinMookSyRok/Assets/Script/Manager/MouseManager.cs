using System;
using Script.Util;
using UnityEngine;

namespace Script.Manager
{
    /*
     * 상호작용 - 점프
     * 몬스터 공격
     * UI
     */
    public enum Task
    {
        Jump,
        Attack,
        None
    }

    public class MouseManager : MonoSingleton<MouseManager>
    {
        [SerializeField] private Texture2D m_DefaultCursor;
        [SerializeField] private Texture2D m_AttackCursor;
        [SerializeField] private Texture2D m_JumpCursor;
        [SerializeField] private LayerMask m_LayerMask;
        
        private Texture2D CurrentCursor { set => Cursor.SetCursor(value, Vector2.zero, CursorMode.Auto); }

        private const string TAG_JUMP = "Jump";
        private const string TAG_ENEMY = "Enemy";

        private void Update()
        {
            MouseAction();
        }

        public Task MouseAction()
        {
            var _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var _hit = Physics2D.Raycast(_target, Vector2.zero, Mathf.Infinity,m_LayerMask);
            if (_hit.collider != null)
            {
                var _tag = _hit.collider.tag;
                if (_tag.Equals(TAG_JUMP))
                {
                    CurrentCursor = m_JumpCursor;
                    return Task.Jump;
                }
                if (_tag.Equals(TAG_ENEMY))
                {
                    CurrentCursor = m_AttackCursor;
                    return Task.Attack;
                }
            }
            CurrentCursor = m_DefaultCursor;
            return Task.None;
        }
    }
}