using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScriptableObject : ScriptableObject
{
    [SerializeField]
    public BuildInfoClass buildInfoClass = new BuildInfoClass();
}
