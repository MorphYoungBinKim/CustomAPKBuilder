using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BuildPropertyRect : EditorWindow
{
    private GUIStyle guiStyle;
    private int tabIndex = 0;
    private string[] tabString = { "Scene", "Event", "File" };

    private string appName;

    public void Init()
    {
        appName = AutoBuilderWindow.Buildinfo.AppName;
        SceneSetting = new List<SceneAsset>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
                SceneSetting.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path));
        }
        
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

            //Handles.DrawLine(new Vector2(rect.x + 10, rect.y), new Vector2(rect.width - 10, rect.y));



            //SerializedProperty customEvent = SerializedObject.FindProperty("OnCheckEvent");
            //AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeBuild.;// = EditorGUILayout.PropertyField(customEvent);
            //SerializedProperty sprop = Editor.serializedObject.FindProperty("myEvent");
            //EditorGUILayout.PropertyField()





            //bug.Log(AutoBuilderWindow.Buildinfo.AppName);

            //TextLabel("APKName", "Build APK Name", AutoBuilderWindow.Buildinfo.AppName);

            //Debug.Log(AutoBuilderWindow.Buildinfo.AppName);
            //GUILayout.Space(-20);
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
            //Debug.Log(AutoBuilderWindow.Buildinfo.AppName);
            GUILayout.EndArea();
            //Refresh();

        }
        catch
        {

        }
    }

    private void Refresh()
    {
        appName = AutoBuilderWindow.Buildinfo.AppName;
    }

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
            //GUILayout.BeginHorizontal();
            GUILayout.Label("Event Setting", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            
            SceneScroll = GUILayout.BeginScrollView(SceneScroll, GUILayout.Height(455));

            if (serializedObject != null)
            {
                Debug.Log(serializedObject.FindProperty("OnBeforeBuild"));
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
        Debug.Log("ResetEvent");
        Debug.Log(AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeBuild);
    }

    private void OnGUI_Setting() { GUILayout.Label("Setting"); }

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
