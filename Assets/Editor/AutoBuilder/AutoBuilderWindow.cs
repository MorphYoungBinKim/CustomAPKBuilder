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
        set
        {
            buildinfo = value;
        }
    }

    public BuildInfoClass ResetSaveData()
    {
        var tempObject = BuildCommonMethod.GetSaveDataObject();

        if (tempObject != null)
        {
            Debug.Log("GetSaveDataObject Complete");
            buildinfo = BuildCommonMethod.SetBuildInfo(tempObject.buildInfoClass);
        }
        else
        {
            Debug.Log("GetSaveDataObject : Create new class");
            buildinfo = new BuildInfoClass();
        }
        return buildinfo;
    }

    /*
    [MenuItem("Build/ResetData", false, 300)]
    public static void TestData()
    {
        if (instance != null)
        {
            instance = null;
            Buildinfo = null;
        }
    }*/

    public bool IsDatainit = false;
    private BuildToolBar toolBar;
    private BuildPropertyRect buildpropertyRect;
    private Vector2 windowSize = new Vector2(500, 700);
    private Rect toolBarRect;
    private Rect propertyRect;


    public void Init()
    {
        minSize = windowSize;
        maxSize = windowSize;
        position = new Rect(80, 150, minSize.x, minSize.y);
        toolBar = new BuildToolBar();
        toolBarRect = new Rect(0, 0, position.width, 18);
        propertyRect = new Rect(5, 25, position.width - 10, 700);
        buildpropertyRect = CreateInstance<BuildPropertyRect>();
        buildpropertyRect.Init();
    }

    public void OnEnable()
    {
        Init();
    }

    private void OnGUI()
    {
        if (!IsDatainit)
        {
            var a = ResetSaveData();
            buildinfo = BuildCommonMethod.SetBuildInfo(a);
            IsDatainit = true;
            buildpropertyRect.Init();
            instance.Repaint();
        }
        toolBar.OnGUI(toolBarRect);
        buildpropertyRect.OnGUIActive(propertyRect);
        if (GUI.Button(new Rect(5, windowSize.y - 35, position.width - 10, 30), "Build"))
        {
            bool isStartBuild = EditorUtility.DisplayDialog("Build Info",
                                                "AppName : " + buildinfo.AppName + "\n" +
                                                "App Version :  " + buildinfo.AppVersion + "\n" +
                                                "Version Code : " + buildinfo.VersionCode + "\n" +
                                                "Build Type :   " + buildinfo.TargetPlatform.ToString() + "\n" +
                                                "Target Type :  " + buildinfo.TargetType
                                                , "빌드 시작", "취소");

            if (isStartBuild)
            {
                BuildAPK();
            }
        }
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
        var apk = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, buildinfo.TargetPlatform == BuildPlatform.Android ? BuildTarget.Android : BuildTarget.iOS, BuildOptions.None);

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
    }

    #region ShowWindow
    [MenuItem("Build/BuildAPK", false, 300)]
    static void ShowWindow()
    {
        if (instance != null)
        {
            instance.Close();
        }
        //buildinfo = Buildinfo;
        instance = GetWindow<AutoBuilderWindow>();

        instance.Show();
    }
    #endregion
}
#endif