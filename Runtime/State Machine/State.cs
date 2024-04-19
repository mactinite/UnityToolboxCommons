using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mactinite.ToolboxCommons.StateMachine
{
    public abstract class State
    {
        private List<StateTransition> _transitions;

        protected State()
        {
            _transitions = new List<StateTransition>();
        }

        public virtual void OnEnter(Blackboard _blackboard)
        {
        }

        public virtual void OnUpdate(Blackboard _blackboard)
        {
        }
        
        public virtual void OnFixedUpdate(Blackboard _blackboard)
        {
        }

        public virtual void OnExit(Blackboard _blackboard)
        {
        }

        public bool EvaluateTransitions(out string destination)
        {
            destination = "";
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i].Evaluate())
                {
                    destination = _transitions[i].Destination;
                    return true;
                }
            }

            return false;
        }

        public State AddTransition(string to, Func<bool> eval)
        {
            _transitions.Add(new StateTransition(to, eval));
            return this;
        }
    }


    public abstract class State<T> : State where T : Blackboard
    {
        public virtual void OnEnter(T _blackboard)
        {
        }

        public override void OnEnter(Blackboard _blackboard)
        {
            T blackboard = (T) _blackboard;
            OnEnter(blackboard);
        }

        public virtual void OnUpdate(T _blackboard)
        {
        }
        
        public override void OnUpdate(Blackboard _blackboard)
        {
            T blackboard = (T) _blackboard;
            OnUpdate(blackboard);
        }
        
        public virtual void OnFixedUpdate(T _blackboard)
        {
        }
        
        public override void OnFixedUpdate(Blackboard _blackboard)
        {
            T blackboard = (T) _blackboard;
            OnFixedUpdate(blackboard);
        }

        public virtual void OnExit(T _blackboard)
        {
        }

        public override void OnExit(Blackboard _blackboard)
        {
            T blackboard = (T) _blackboard;
            OnExit(blackboard);
        }
    }
}