using System;
using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

namespace mactinite.ToolboxCommons.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        public Blackboard _blackboard = new Blackboard();

        private string _currentStateKey = null;
        private State _currentState = null;
        private Dictionary<string, State> states = new Dictionary<string, State>();
        public string CurrentStateName => _currentStateKey;
        public event Action<string> OnStateChange;

        private void Awake()
        {

        }

        private void Update()
        {
            if (_currentState == null) return;


            if (OnStateChange != null)
            {
                Debug.Log($"Subscribers: {OnStateChange.GetInvocationList().Length}");

                foreach (var invocation in OnStateChange.GetInvocationList())
                {
                    Debug.Log($"{invocation}");
                }
            }

            // Run the update on the current state.
            _currentState.OnUpdate(_blackboard);
            
            // evaluate transitions and move to next state.
            if (_currentState.EvaluateTransitions(out var destination))
            {
                TransitionTo(destination);
            }
        }

        private void SetState(State state)
        {
            // process the exit event of the last state
            if (_currentState != null)
                _currentState.OnExit(_blackboard);

            // update and start the enter event of the new state
            _currentState = state;
            _currentState.OnEnter(_blackboard);
        }

        public void TransitionTo(string stateName)
        {
            if (states.TryGetValue(stateName, out var nextState))
            {
                SetState(nextState);
                OnStateChange?.Invoke(stateName);
            }
            else
            {
                throw new Exception(
                    $"{stateName} not found in runtime state map. \n" +
                    $"Did you forget to register the state or use the wrong identifier?");
            }
        }
        
        public void RegisterState(string stateName, State state)
        {
            states.Add(stateName, state);
        }
        
        public State RegisterState<T>(string stateName) where T: State, new()
        {
            states.Add(stateName, new T());
            return states[stateName];
        }
    }
}