using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BuildPropertyRect : EditorWindow
{
    [SerializeField]
    public List<SceneAsset> SceneSetting;
    public Vector2 SceneScroll;

    public UnityEvent OnBeforeBuild;
    public UnityEvent OnAfterBuild;
    public UnityEvent OnBeforeProductBuild;
    public UnityEvent OnAfterProductBuild;
    public UnityEvent OnBeforeStageBuild;
    public UnityEvent OnAfterStageBuild;

    private SerializedObject serializedObject;
    private GUIStyle guiStyle;
    private int tabIndex = 0;
    private string[] tabString = { "Scene", "Event", "Setting" };

    public void Init()
    {
        SceneSetting = AutoBuilderWindow.Buildinfo.SceneSetting;
        OnBeforeBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeBuild;
        OnAfterBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterBuild;
        OnBeforeProductBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeProductBuild;
        OnAfterProductBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterProductBuild;
        OnBeforeStageBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeStageBuild;
        OnAfterStageBuild = AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterStageBuild;
        serializedObject = new SerializedObject(this);
    }

    public void OnGUIActive(Rect rect)
    {
        try
        {
            GUILayout.BeginArea(rect);
            GUILayout.Space(5);
            GUILayout.Label("APK Info", EditorStyles.boldLabel);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("APKName", "Build APK Name");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.AppName = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.AppName);
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("APKPath", "Build APK Path");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.BuildPath = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.BuildPath);
            if (GUILayout.Button("Path"))
            {
                AutoBuilderWindow.Buildinfo.BuildPath = EditorUtility.OpenFolderPanel("Path", "", "");
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("AppVersion", "Build APK Version");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.AppVersion = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.AppVersion, GUILayout.Width(100));
            GUILayout.Space(40);
            TitleToolTip("VersionCode", "APK Version Code");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.VersionCode = EditorGUILayout.IntField(AutoBuilderWindow.Buildinfo.VersionCode, GUILayout.Width(100));
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("Build Target", "Build Platform Target");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.TargetPlatform = (BuildPlatform)EditorGUILayout.EnumPopup(AutoBuilderWindow.Buildinfo.TargetPlatform);
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("Build Type", "Build APK Type");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.TargetType = (BuildType)EditorGUILayout.EnumPopup(AutoBuilderWindow.Buildinfo.TargetType);
            GUILayout.EndHorizontal();
            GUILayout.Space(3);

            if (GUI.changed)
            {
                PlayerSettings.bundleVersion = AutoBuilderWindow.Buildinfo.AppVersion;
                PlayerSettings.Android.bundleVersionCode = AutoBuilderWindow.Buildinfo.VersionCode;
            }

            GUILayout.Space(3);
            tabIndex = GUILayout.Toolbar(tabIndex, tabString);

            switch (tabIndex)
            {
                case 0:
                    OnGUI_Scene();
                    break;
                case 1:
                    OnGUI_View();
                    break;
                case 2:
                    OnGUI_Setting();
                    break;
            }

            GUILayout.EndArea();

        }
        catch
        {

        }
    }

    private void OnGUI_Scene()
    {
        try
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            SceneScroll = GUILayout.BeginScrollView(SceneScroll, GUILayout.Height(470));
            if (serializedObject != null)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SceneSetting"), GUILayout.Width(460));
                serializedObject.ApplyModifiedProperties();

            }

            if (GUI.changed)
            {
                ApplySceneData();
            }

            GUILayout.EndScrollView();
            GUILayout.EndHorizontal();
        }
        catch
        {


        }

    }

    public void ApplySceneData()
    {
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
        foreach (var sceneAsset in SceneSetting)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            if (!string.IsNullOrEmpty(scenePath))
                editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
        }
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
    }

    private void OnGUI_View()
    {
        try
        {
            GUILayout.Space(5);
            GUILayout.Label("Event Setting", EditorStyles.boldLabel);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            SceneScroll = GUILayout.BeginScrollView(SceneScroll, GUILayout.Height(445));

            if (serializedObject != null)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnBeforeBuild"), GUILayout.Width(460));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnAfterBuild"), GUILayout.Width(460));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnBeforeProductBuild"), GUILayout.Width(460));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnAfterProductBuild"), GUILayout.Width(460));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnBeforeStageBuild"), GUILayout.Width(460));
                GUILayout.Space(10);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OnAfterStageBuild"), GUILayout.Width(460));

                serializedObject.ApplyModifiedProperties();

            }

            if (GUI.changed)
            {
                ApplyEventData();
            }
            GUILayout.EndScrollView();
            GUILayout.EndHorizontal();

        }
        catch
        {

        }
    }

    public void ApplyEventData()
    {
        AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeBuild = OnBeforeBuild;
        AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterBuild = OnAfterBuild;
        AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeProductBuild = OnBeforeProductBuild;
        AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterProductBuild = OnAfterProductBuild;
        AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeStageBuild = OnBeforeStageBuild;
        AutoBuilderWindow.Buildinfo.BuildEvent.OnAfterStageBuild = OnAfterStageBuild;
    }

    private void OnGUI_Setting()
    {
        GUILayout.Space(5);
        GUILayout.Label("Custom Setting", EditorStyles.boldLabel);
        SceneScroll = GUILayout.BeginScrollView(SceneScroll, GUILayout.Height(455));
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        TitleToolTip("Company Name", "");
        GUILayout.Space(-20);
        AutoBuilderWindow.Buildinfo.CompanyName = GUILayout.TextField(AutoBuilderWindow.Buildinfo.CompanyName);
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        TitleToolTip("Product Name", "");
        GUILayout.Space(-20);
        AutoBuilderWindow.Buildinfo.ProductName = GUILayout.TextField(AutoBuilderWindow.Buildinfo.ProductName);
        GUILayout.EndHorizontal();
        GUILayout.Space(20);
        if (AutoBuilderWindow.Buildinfo.TargetPlatform == BuildPlatform.Android)
        {
            GUILayout.Label("Android SDK", EditorStyles.boldLabel);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("MinSDK", "");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.minSdkVersion = (AndroidSdkVersions)EditorGUILayout.EnumPopup(AutoBuilderWindow.Buildinfo.minSdkVersion);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("Target SDK", "");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.targetSdkVersion = (AndroidSdkVersions)EditorGUILayout.EnumPopup(AutoBuilderWindow.Buildinfo.targetSdkVersion);
            GUILayout.EndHorizontal();
            GUILayout.Space(20);

            if(GUI.changed)
            {
                PlayerSettings.productName = AutoBuilderWindow.Buildinfo.ProductName;
                PlayerSettings.companyName = AutoBuilderWindow.Buildinfo.CompanyName;
                PlayerSettings.Android.minSdkVersion = AutoBuilderWindow.Buildinfo.minSdkVersion;
                PlayerSettings.Android.targetSdkVersion = AutoBuilderWindow.Buildinfo.targetSdkVersion;
            }
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Manifest", EditorStyles.boldLabel);
        GUILayout.Space(-70);

        if (AutoBuilderWindow.Buildinfo.UseSchema = EditorGUILayout.Toggle(AutoBuilderWindow.Buildinfo.UseSchema))
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("Schema Name", "Use Manifest for Schema");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.SchemaName = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.SchemaName, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }
        else
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUI.enabled = false;
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("Schema Name", "Use Manifest for Schema");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.SchemaName = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.SchemaName, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
            GUI.enabled = true;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("Key Store", EditorStyles.boldLabel);
        GUILayout.Space(-75);

        if (AutoBuilderWindow.Buildinfo.UseKeyStore = EditorGUILayout.Toggle(AutoBuilderWindow.Buildinfo.UseKeyStore))
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("KeyStore Path", "KeyStore Local Path");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.KeyStorePath = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.KeyStorePath, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("KeyStore PassWord", "KeyStore PassWord");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.KeyStorePassWord = EditorGUILayout.PasswordField(AutoBuilderWindow.Buildinfo.KeyStorePath, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
        else
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUI.enabled = false;
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("KeyStore Path", "KeyStore Local Path");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.KeyStorePath = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.KeyStorePath, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(3);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("KeyStore PassWord", "KeyStore PassWord");
            GUILayout.Space(-20);
            AutoBuilderWindow.Buildinfo.KeyStorePassWord = EditorGUILayout.PasswordField(AutoBuilderWindow.Buildinfo.KeyStorePath, GUILayout.Width(330));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUI.enabled = true;
        }

        if (GUI.changed)
        {
            PlayerSettings.companyName = AutoBuilderWindow.Buildinfo.CompanyName;
            PlayerSettings.productName = AutoBuilderWindow.Buildinfo.ProductName;
        }

        GUILayout.EndScrollView();
    }

    protected void TitleToolTip(string title, string tootip)
    {
        var guicontent = new GUIContent(title, tootip);
        GUILayout.Label(guicontent, GUILayout.Width(150));
    }

    protected void TextLabel(string title, string tooltip, string type)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        TitleToolTip(title, tooltip);
        GUILayout.Space(-60);
        EditorGUILayout.TextField(type);
        GUILayout.EndHorizontal();
    }
}
