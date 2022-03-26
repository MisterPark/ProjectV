using System;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class DictionaryInInspectorAttribute : PropertyAttribute
{
    public Type keyType;
    public Type valueType;
    public DictionaryInInspectorAttribute(Type _keyType, Type _valueType)
    {
        keyType = _keyType;
        valueType = _valueType;
    }
}

[CustomPropertyDrawer(typeof(DictionaryInInspectorAttribute))]
public class DictionaryInInspectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, new GUIContent("aaa", label.tooltip), true);
    }
}
#endif