#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.Events;
using System;

public class AutoBuilderWindow : EditorWindow
{
    private static AutoBuilderWindow instance;
    public static AutoBuilderWindow GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GetWindow<AutoBuilderWindow>();
            }
            return instance;
        }
    }

    private static BuildInfoClass buildinfo;
    public static BuildInfoClass Buildinfo
    {
        get
        {
            if (buildinfo == null)
            {
                buildinfo = new BuildInfoClass();
            }
            return buildinfo;
        }
    }

    private BuildToolBar toolBar;
    private BuildPropertyRect buildpropertyRect;
    private Vector2 windowSize = new Vector2(500, 700);
    private Rect toolBarRect;
    private Rect propertyRect;

    //private IList serializedObject;
    //SerializedObject serializedObject = new SerializedObject();
    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        minSize = windowSize;
        maxSize = windowSize;
        position = new Rect(80, 150, minSize.x, minSize.y);
        toolBar = new BuildToolBar();
        toolBarRect = new Rect(0, 0, position.width, 18);
        propertyRect = new Rect(5, 25, position.width-10, 700);
        buildpropertyRect = CreateInstance<BuildPropertyRect>();
        buildpropertyRect.Init();
        //editorScript = new EditorScript();
        //editorRect= new Rect(0, 18, position.width, 100);
        //ReorderableList reorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("events"), true, true, true, true);
        //List = new ReorderableList(serializedObject, serializedObject.FindProperty("events"), true, true, true, true);
    }

    private void OnGUI()
    {

        //var guiStyle = new GUIStyle(EditorStyles.helpBox);
        //GUI.Box(new Rect(0, 0, maxSize.x, maxSize.y), "", guiStyle);
   
        toolBar.OnGUI(toolBarRect);
        buildpropertyRect.OnGUIActive(propertyRect);
        if (GUI.Button(new Rect(5, windowSize.y - 35, position.width - 10, 30), "Build"))
        {
            BuildAPK();
        }
        //editorScript.OnGUI(editorRect);
    }

    private void BuildAPK()
    {
        PlayerSettings.SplashScreen.show = false;

        if (Buildinfo.BuildEvent.OnBeforeBuild != null)
            Buildinfo.BuildEvent.OnBeforeBuild.Invoke();

        switch (Buildinfo.TargetType)
        {

            case BuildType.None:
            case BuildType.Product:
                if (Buildinfo.BuildEvent.OnBeforeProductBuild != null)
                    Buildinfo.BuildEvent.OnBeforeProductBuild.Invoke();
                    break;

            case BuildType.Stage:
                if (Buildinfo.BuildEvent.OnBeforeStageBuild != null)
                    Buildinfo.BuildEvent.OnBeforeStageBuild.Invoke();
                break;

        }

        string Date = DateTime.Now.ToString("yyyyMMdd");

        string path = string.Format("{0}/{1}_v{2}_{3}_{4}.apk", buildinfo.BuildPath, buildinfo.AppName, buildinfo.AppVersion, Buildinfo.TargetType == BuildType.Product ? "Prod" : "STG", Date);
        Debug.Log(path);
        var apk = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path , buildinfo.TargetPlatform == BuildPlatform.Android ? BuildTarget.Android : BuildTarget.iOS , BuildOptions.None);

        if (Buildinfo.BuildEvent.OnAfterBuild != null)
            Buildinfo.BuildEvent.OnAfterBuild.Invoke();

        switch (Buildinfo.TargetType)
        {

            case BuildType.None:
            case BuildType.Product:
                if (Buildinfo.BuildEvent.OnAfterProductBuild != null)
                    Buildinfo.BuildEvent.OnAfterProductBuild.Invoke();
                break;

            case BuildType.Stage:
                if (Buildinfo.BuildEvent.OnAfterStageBuild != null)
                    Buildinfo.BuildEvent.OnAfterStageBuild.Invoke();
                break;

        }

        System.Diagnostics.Process.Start(buildinfo.BuildPath);
        //var apk = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path  + ".APK", buildinfo.TargetPlatform, BuildOptions.None);
        //BuildPipeline.BuildA
        //EditorBuildSettings.and
        //var Scenes = PlayerSettings.Scene

        //Debug.Log(buildinfo.BuildPath + "/" + buildinfo.AppName + ".APK");
        //BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildinfo.BuildPath + "/" + buildinfo.AppName + ".APK",buildinfo.TargetPlatform,BuildOptions.None);
    }

    #region ShowWindow
    [MenuItem("Build/BuildAPK", false, 300)]
    static void ShowWindow()
    {
        if (instance != null)
        {
            instance.Close();
        }

        instance = GetWindow<AutoBuilderWindow>();
        instance.Show();
    }
    #endregion
}
#endif