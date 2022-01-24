using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[SerializeField]
public class BuildInfoClass
{
    public string AppName;
    public string BuildPath;
    public int AppVersion;
    public int VersionCode;
    public BuildType TargetType;
    public BuildTarget TargetPlatform;
    public BuildEventClass BuildEvent;

}

[SerializeField]
public enum BuildType
{
    None =0,
    Product = 1,
    Stage = 2,
}

[SerializeField]
public class BuildEventClass
{
    public Action OnBeforeBuild;
    public Action OnAfterBuild;
    public Action OnBeforeProductBuild;
    public Action OnAfterProductBuild;
    public Action OnBeforeStageBuild;
    public Action OnAfterStageBuild;
}
