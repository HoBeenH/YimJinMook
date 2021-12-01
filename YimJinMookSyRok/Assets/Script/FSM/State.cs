using UnityEngine;

namespace Script.FSM
{
    public abstract class State<T>
    {
        #region Constructor

        protected State()
        {
        }

        protected State(string animHash) : this(Animator.StringToHash(animHash))
        {
        }

        private State(int animHash) => this.animHash = animHash;

        #endregion

        public readonly int animHash;
        protected StateMachine<T> machine;
        protected T owner;

        public void AddState(T currentOwner, StateMachine<T> currentMachine)
        {
            this.machine = currentMachine;
            this.owner = currentOwner;
            Init();
        }

        protected virtual void Init()
        {
        }

        public virtual void OnStateEnter()
        {
        }

        public virtual void OnStateUpdate()
        {
        }

        public virtual void OnStateChangePoint()
        {
        }

        public virtual void OnStateExit()
        {
        }
    }
}