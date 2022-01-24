#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildToolBar
{
    private GUIStyle guiStyle;

    private Texture2D createTexture;
    private Texture2D refreshTexture;
    private Texture2D deleteTexture;

    public void OnGUI(Rect rect)
    {
        guiStyle = new GUIStyle(EditorStyles.toolbarButton);
        guiStyle.fixedHeight = rect.height;
        try
        {
            GUI.Box(rect, "", new GUIStyle(EditorStyles.toolbar));
            GUILayout.BeginArea(rect);
            GUILayout.BeginHorizontal();
            GUILayout.Box("", guiStyle);
            Create();
            Refresh();
            Load();
            Delete();
            GUILayout.Space(30);
            //SearchField();
            GUILayout.Space(30);
            GUILayout.FlexibleSpace();
            /*
            if (trpBuildFormat != null)
            {
                OpenExplore();
                BuildType();
                UpLoad();
                Save();
                Build();
            }*/
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        catch
        {

        }
    }
    private void Create()
    {
        var guicontent = new GUIContent("Save", "Build ���� ����");
        if (GUILayout.Button(guicontent, guiStyle))
        {
            int count = 0;
        }
    }

    /// <summary>
    /// Refresh Button
    /// </summary>
    private void Refresh()
    {
        var guicontent = new GUIContent("Refresh", "���ΰ�ħ");
        if (GUILayout.Button(guicontent, guiStyle))
        {

        }
    }

    private void Load()
    {
        var guicontent = new GUIContent("Load", "Build ���� �ҷ�����");
        if (GUILayout.Button(guicontent, guiStyle))
        {

        }
    }

    /// <summary>
    /// Delete Button
    /// </summary>
    private void Delete()
    {
        var guicontent = new GUIContent("Delete", "Build â �ʱ�ȭ");

        if (GUILayout.Button(guicontent, guiStyle))
        {

        }
    }
}
#endif
