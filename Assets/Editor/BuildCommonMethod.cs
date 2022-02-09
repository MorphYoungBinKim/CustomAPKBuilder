using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class BuildCommonMethod
{

}

[CustomPropertyDrawer(typeof(UnityEventBase), true)]
public class ReorderingUnityEventDrawer : UnityEventDrawer
{
    protected override void SetupReorderableList(ReorderableList list)
    {
        base.SetupReorderableList(list);

        list.draggable = true;
    }
}

public static class ReorderableListUtility
{
    public static ReorderableList CreateReorderableListSimple(SerializedObject serializedObject, SerializedProperty elements)
    {
        var reorderableList = new ReorderableList(serializedObject, elements);
        reorderableList.drawHeaderCallback += (rect) => DrawHeaderCallback(rect, elements.name);
        reorderableList.drawElementCallback += (rect, index, isActive, isFocused) => DrawElementCallaback(rect, index, isActive, isFocused, elements);

        return reorderableList;
    }

    private static void DrawHeaderCallback(Rect rect, string label)
    {
        EditorGUI.LabelField(rect, label);
    }

    private static void DrawElementCallaback(Rect rect, int index, bool isActive, bool isFocused, SerializedProperty elements)
    {
        EditorGUI.PropertyField(rect, elements.GetArrayElementAtIndex(index), GUIContent.none);
    }
}