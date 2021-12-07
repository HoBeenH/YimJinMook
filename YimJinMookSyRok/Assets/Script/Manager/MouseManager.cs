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
    public class MouseManager : MonoSingleton<MouseManager>
    {
        /*
         *
        [SerializeField] private Texture2D m_DefaultCursor;
        [SerializeField] private Texture2D m_AttackCursor;
        
        private Texture2D CurrentCursor { set => Cursor.SetCursor(value, Vector2.zero, CursorMode.Auto); }
         * 
         */

        [SerializeField] private const string TAG_JUMP = "Jump";
        public bool Test = false;

        private void Update()
        {
            MousePos();
        }

        private void MousePos()
        {
            var _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var _hit = Physics2D.Raycast(_target, Vector2.zero, Mathf.Infinity);

            if (_hit.collider != null && Input.GetMouseButtonDown(0))
            {
                var _tag = _hit.collider.tag;
                if (_tag.Equals(TAG_JUMP))
                {
                    Test = true;
                }
            }
            else
            {
                Test = false;
            }
        }
    }
}