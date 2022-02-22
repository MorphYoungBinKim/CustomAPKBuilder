using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable", menuName = "Test",order =2)]
public class SaveScriptableObject : ScriptableObject
{
    [SerializeField]
    public BuildInfoClass buildInfoClass = AutoBuilderWindow.Buildinfo;
}
