using System;

namespace mactinite.ToolboxCommons.StateMachine
{
    public class StateTransition
    {
        private Func<bool> evaluationDelegate;
        private string _destinationState;
        public string Destination => _destinationState;

        public StateTransition(string destination, Func<bool> eval)
        {
            evaluationDelegate = eval;
            _destinationState = destination;
        }
        
        public bool Evaluate()
        {
            return evaluationDelegate();
        }
    }
}