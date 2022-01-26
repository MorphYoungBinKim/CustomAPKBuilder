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
       /* guiStyle = new GUIStyle(EditorStyles.);
        guiStyle.fixedHeight = rect.height;
        GUI.Box(rect, "", new GUIStyle(EditorStyles.toolbar));*/
        try
        {
            GUILayout.BeginArea(rect);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("APKName", "Build APK Name");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.AppName = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.AppName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            TitleToolTip("APKPath", "Build APK Name");
            GUILayout.Space(-60);
            AutoBuilderWindow.Buildinfo.BuildPath = EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.BuildPath);
            if (GUILayout.Button("Path"))
            {
                AutoBuilderWindow.Buildinfo.BuildPath = EditorUtility.OpenFolderPanel("Path", "","");
            }
            GUILayout.EndHorizontal();
            //SerializedProperty customEvent = SerializedObject.FindProperty("OnCheckEvent");
            //AutoBuilderWindow.Buildinfo.BuildEvent.OnBeforeBuild.;// = EditorGUILayout.PropertyField(customEvent);
            //SerializedProperty sprop = Editor.serializedObject.FindProperty("myEvent");
            //EditorGUILayout.PropertyField()





            //bug.Log(AutoBuilderWindow.Buildinfo.AppName);

            //TextLabel("APKName", "Build APK Name", AutoBuilderWindow.Buildinfo.AppName);

            //Debug.Log(AutoBuilderWindow.Buildinfo.AppName);
            //GUILayout.Space(-20);
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
