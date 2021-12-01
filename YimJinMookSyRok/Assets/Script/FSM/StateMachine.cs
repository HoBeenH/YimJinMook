using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.FSM
{
    public class StateMachine<T>
    {
        private readonly Dictionary<Type, State<T>> m_States = new Dictionary<Type, State<T>>();
        private State<T> CurrentState { get; set; }
        private readonly T m_Owner;

        public readonly Animator anim;

        public StateMachine(State<T> state, T owner, Animator anim)
        {
            m_Owner = owner;
            CurrentState = state;
            this.anim = anim;
            AddState(state);
            CurrentState?.OnStateEnter();
        }

        private void AddState(State<T> state)
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
