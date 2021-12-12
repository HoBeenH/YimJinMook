using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.FSM
{
    public class StateMachine<T>
    {
        private readonly Dictionary<Type, State<T>> m_States = new Dictionary<Type, State<T>>();
        private State<T> CurrentState { get; set; }

        public Type StateType => CurrentState.GetType();

        private readonly T m_Owner;
        private const string IDLE = "Base Layer.Player_Idle";
        private readonly WaitUntil m_Idle;

        public readonly Animator anim;

        public StateMachine(State<T> state, T owner, Animator anim)
        {
            m_Owner = owner;
            CurrentState = state;
            this.anim = anim;
            AddState(state);
            CurrentState?.OnStateEnter();
            m_Idle = new WaitUntil(
                () => anim.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash(IDLE));
        }

        public IEnumerator WaitIdle(Type type, params int[] state)
        {
            foreach (var s in state)
            {
                yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).fullPathHash == s);
            }

            yield return m_Idle;
            ChangeState(type);
        }

        public void AddState(State<T> state)
        {
            state.AddState(m_Owner, this);
            m_States.Add(state.GetType(), state);
        }

        public void OnUpdate()
        {
            var _anim = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            if (_anim == CurrentState?.animHash || CurrentState?.animHash == 0)
            {
                CurrentState?.OnStateChangePoint();
                var _state = CurrentState;
                if (_state == CurrentState)
                {
                    CurrentState?.OnStateUpdate();
                }
            }
        }

        public void ChangeState(Type state)
        {
            if (state == CurrentState.GetType())
            {
                return;
            }

            CurrentState?.OnStateExit();
            CurrentState = m_States[state];
            CurrentState?.OnStateEnter();
        }
    }
}