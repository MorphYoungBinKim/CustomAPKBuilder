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