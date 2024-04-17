using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace mactinite.ToolboxCommons.StateMachine.Editor
{
    [CustomEditor(typeof(StateMachine), true)]
    public class StateMachineInspector : UnityEditor.Editor
    {
        [SerializeField]
        private VisualTreeAsset uxml;

        public override VisualElement CreateInspectorGUI()
        {
            uxml = (VisualTreeAsset)AssetDatabase.LoadAssetAtPath("Packages/com.mactinite.toolboxcommons/Editor/StateMachine_Inspector.uxml", typeof(VisualTreeAsset));

            StateMachine sm = serializedObject.targetObject as StateMachine;

            // Create a new VisualElement to be the root of our Inspector UI.
            VisualElement myInspector = new VisualElement();
            uxml.CloneTree(myInspector);


            sm.OnStateChange += delegate(string s)
            {
                Label currStateLabel = myInspector.Q<Label>("Current_State");
                currStateLabel.text = s;
            };
            
            
            VisualElement InspectorFoldout = myInspector.Q("Default_Inspector");
            
            // Attach a default Inspector to the Foldout.
            InspectorElement.FillDefaultInspector(InspectorFoldout, serializedObject, this);

            // Return the finished Inspector UI.
            return myInspector;
        }

    }
}