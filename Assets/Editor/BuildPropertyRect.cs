using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BuildPropertyRect
{
    private GUIStyle guiStyle;
    private int tabIndex = 0;
    private string[] tabString = { "Event", "File" };

    private string appName;// { get { return appName; } set { AutoBuilderWindow.Buildinfo.AppName = value; } }

    public void Init()
    {
        appName = AutoBuilderWindow.Buildinfo.AppName;
    }

    public void OnGUI(Rect rect)
    {
        try
        {
            GUILayout.BeginArea(rect);
            GUILayout.Space(5);
            GUILayout.Label("APK Info",EditorStyles.boldLabel);
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
                AutoBuilderWindow.Buildinfo.BuildPath = EditorUtility.OpenFolderPanel("Path", "","");
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
                    OnGUI_Main();
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

    private void OnGUI_Main() { GUILayout.Label("Main"); }
    private void OnGUI_View() { GUILayout.Label("View"); }
    private void OnGUI_Setting() { GUILayout.Label("Setting"); }

    protected void TitleToolTip(string title, string tootip)
    {
        var guicontent = new GUIContent(title, tootip);
        GUILayout.Label(guicontent, GUILayout.Width(150));
    }

    protected void TextLabel(string title , string tooltip , string type)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        TitleToolTip(title, tooltip);
        GUILayout.Space(-60);
        EditorGUILayout.TextField(type);
        GUILayout.EndHorizontal();
    }
}
