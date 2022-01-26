using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildPropertyRect
{
    private GUIStyle guiStyle;
    private int tabIndex = 0;
    private string[] tabString = { "Event", "File" };

    public void OnGUI(Rect rect)
    {
       /* guiStyle = new GUIStyle(EditorStyles.);
        guiStyle.fixedHeight = rect.height;
        GUI.Box(rect, "", new GUIStyle(EditorStyles.toolbar));*/
        try
        {
            GUILayout.BeginArea(rect);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            TitleToolTip("APKName", "Build APK Name");
            GUILayout.Space(-60);
            EditorGUILayout.TextField(AutoBuilderWindow.Buildinfo.AppName);
            GUILayout.EndHorizontal();

            Debug.Log(AutoBuilderWindow.Buildinfo.AppName);

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
            GUILayout.EndArea();
        }
        catch
        {

        }
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
