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
    public string AppName;
    public string BuildPath;
    public string AppVersion;
    public int VersionCode;
    public BuildType TargetType;
    public BuildPlatform TargetPlatform;

    //BuildEvent
    public BuildEventClass BuildEvent;

    //KeyStore
    public bool UseKeyStore;
    public string KeyStorePath;
    public string KeyStorePassWord;

    // manifest
    public bool UseSchema;
    public string SchemaName;

    public BuildInfoClass()
    {
        AppName = Application.productName;
        BuildPath = Application.dataPath.Replace("/Assets","");
        AppVersion = Application.version;
        TargetType = BuildType.Product;
        TargetPlatform = BuildPlatform.Android;
        BuildEvent = new BuildEventClass();
        UseKeyStore = false;
        KeyStorePath = "";
        KeyStorePassWord = "";
        UseSchema = false;
        SchemaName = "";
    }
}

[SerializeField]
public enum BuildType
{
    None = 0,
    Product = 1,
    Stage = 2,
}

[SerializeField]
public enum BuildPlatform
{
    Android = 0,
    iOS = 1,
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
