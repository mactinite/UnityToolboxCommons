﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mactinite.ToolboxCommons.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    class LayerAttributeEditor : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // One line of  oxygen free code.
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}