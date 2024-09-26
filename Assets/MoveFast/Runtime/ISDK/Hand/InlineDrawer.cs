// Copyright (c) Meta Platforms, Inc. and affiliates.



using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
#if UNITY_EDITOR
    class InlineDrawer : UnityEditor.PropertyDrawer
    {
        public override float GetPropertyHeight(UnityEditor.SerializedProperty _, GUIContent __) => UnityEditor.EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect rect, UnityEditor.SerializedProperty prop, GUIContent label)
        {
            var enumerator = prop.GetEnumerator();
            enumerator.MoveNext();
            prop = enumerator.Current as UnityEditor.SerializedProperty;
            UnityEditor.EditorGUI.PropertyField(rect, prop, label);
        }
    }
#endif
}
