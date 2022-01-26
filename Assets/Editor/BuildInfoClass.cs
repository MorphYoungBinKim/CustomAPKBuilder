using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[SerializeField]
public class BuildInfoClass
{
    // APK 기본 정보
    public string AppName;// = Application.productName;
    public string BuildPath;// = Application.dataPath;
    public string AppVersion;// = Application.version;
    public int VersionCode;
    public BuildType TargetType;
    public BuildTarget TargetPlatform;
    
    //BuildEvent
    public BuildEventClass BuildEvent;

    //KeyStore
    public bool UseKeyStore;
    public string KeyStorePath;
    public string KeyStorePassWord;

    // manifest
    public bool UseSchema;
    public string SchemaName;
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
    public UnityEvent OnBeforeBuild;
    public UnityEvent OnAfterBuild;
    public UnityEvent OnBeforeProductBuild;
    public UnityEvent OnAfterProductBuild;
    public UnityEvent OnBeforeStageBuild;
    public UnityEvent OnAfterStageBuild;
}
