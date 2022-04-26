using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable,SerializeField]
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
    [SerializeField]
    public BuildEventClass BuildEvent;

    public List<SceneAsset> SceneSetting;

    // other Setting
    public string CompanyName;
    public string ProductName;
    public AndroidSdkVersions minSdkVersion;
    public AndroidSdkVersions targetSdkVersion;

    //KeyStore
    public bool UseKeyStore;
    public string KeyStorePath;
    public string KeyStorePassWord;

    // manifest
    public bool UseSchema;
    public string SchemaName;

    public BuildInfoClass()
    {
        try
        { 
        AppName = Application.productName;
            BuildPath = Application.dataPath.Replace("/Assets", "");
            AppVersion = PlayerSettings.bundleVersion;
            VersionCode = PlayerSettings.Android.bundleVersionCode;
            TargetType = BuildType.Product;
            TargetPlatform = BuildPlatform.Android;
            BuildEvent = new BuildEventClass();
            SceneSetting = new List<SceneAsset>();
            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    SceneSetting.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path));
            }
            CompanyName = PlayerSettings.companyName;
            ProductName = PlayerSettings.productName;
            minSdkVersion = PlayerSettings.Android.minSdkVersion;
            targetSdkVersion = PlayerSettings.Android.targetSdkVersion;
            UseKeyStore = false;
            KeyStorePath = "";
            KeyStorePassWord = "";
            UseSchema = false;
            SchemaName = "";
        }
        catch
        {

        }
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

[Serializable]
public class BuildEventClass
{
    public UnityEvent OnBeforeBuild;
    public UnityEvent OnAfterBuild;
    public UnityEvent OnBeforeProductBuild;
    public UnityEvent OnAfterProductBuild;
    public UnityEvent OnBeforeStageBuild;
    public UnityEvent OnAfterStageBuild;
}
