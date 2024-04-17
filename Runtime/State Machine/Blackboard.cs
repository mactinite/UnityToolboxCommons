using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using UnityEngine;

namespace mactinite.ToolboxCommons.StateMachine
{
    [Serializable]
    public class Blackboard
    {
        public Dictionary<string, object> variables;
        
        public event Action OnBlackboardUpdate;
        public Blackboard()
        {
            variables = new Dictionary<string, object>();
        }

        public void Set(string key, object value)
        {
            if (variables.ContainsKey(key))
            {
                variables[key] = value;
            }
            else
            {
                variables.Add(key, value);
            }

            OnBlackboardUpdate?.Invoke();
        }

        public T? Get<T>(string key) where T : struct
        {
            if (variables.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            return null;
        }
    }
}