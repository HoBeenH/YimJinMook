using System.Collections;
using Script.Util;
using Unity.Collections;
using Unity.Jobs;
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
        
        #endregion

        [SerializeField] private Slider healthUI;
        [SerializeField] private Slider staminaUI;

        public float healthValue
        {
            set => healthUI.value = value;
        }

        private float staminaValue
        {
            set => staminaUI.value = value;
        }

        private Coroutine m_Stamina;

        
        private void Awake()
        {
            m_DefaultCursor = Resources.Load<Texture2D>("Cursor/Default");
            m_AttackCursor = Resources.Load<Texture2D>("Cursor/Attack");
            m_JumpCursor = Resources.Load<Texture2D>("Cursor/Jump");
            EventSystem.BindEvent(Task.Attack, sender => Cursor.SetCursor(m_AttackCursor, Vector2.zero, CursorMode.Auto));
            EventSystem.BindEvent(Task.Jump, sender => Cursor.SetCursor(m_JumpCursor, Vector2.zero, CursorMode.Auto));
            EventSystem.BindEvent(Task.None, sender => Cursor.SetCursor(m_DefaultCursor, Vector2.zero, CursorMode.Auto));
        }

        private void Update()
        {
            UIValueChange();
        }

        private void UIValueChange()
        {
            CalCuJob(out var stamina, out var health);
            healthValue = health;
            staminaValue = stamina;
        }

        private struct StaminaJob : IJobParallelFor
        {
            [WriteOnly] public NativeArray<float> returnValue;
            [ReadOnly] public NativeArray<float> max;
            [ReadOnly] public NativeArray<float> curValue;

            public void Execute([ReadOnly]int i)
            {
                returnValue[i] = curValue[i] / max[i];
            }
        }

        private void CalCuJob(out float stamina, out float health)
        {
            var _value = new NativeArray<float>(2, Allocator.TempJob);
            var _max = new NativeArray<float>(2, Allocator.TempJob);
            _max[0] = _PlayerController.Stat.maxHealth;
            _max[1] = _PlayerController.Stat.maxStamina;
            var _cur = new NativeArray<float>(2, Allocator.TempJob);
            _cur[0] = _PlayerController.Stat.Health;
            _cur[1] = _PlayerController.Stat.Stamina;
            var _job = new StaminaJob()
            {
                returnValue = _value,
                max = _max,
                curValue = _cur
            };
            var _handle = _job.Schedule(2, 100);
            _handle.Complete();
            _max.Dispose();
            _cur.Dispose();
            health = _value[0];
            stamina = _value[1];
            _value.Dispose();
        }
    }
}