#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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
            Save();
            Load();
            Export();
            GUILayout.Space(30);
            GUILayout.Space(30);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        catch
        {

        }
    }
    private void Save()
    {
        var guicontent = new GUIContent("Save", "Build ���� ����");
        if (GUILayout.Button(guicontent, guiStyle))
        {
            BuildCommonMethod.SaveDataObject();
        }
    }

    /// <summary>
    /// Refresh Button
    /// </summary>
    private void Export()
    {
        var guicontent = new GUIContent("Export", "��������");
        if (GUILayout.Button(guicontent, guiStyle))
        {
            string dataPath = EditorUtility.OpenFolderPanel("Path", "", "");

            if (!string.IsNullOrWhiteSpace(dataPath) && Directory.Exists(dataPath))
            {
                //BuildCommonMethod.GetSaveDataObject();
                File.Copy(BuildCommonMethod.SaveFilePath, dataPath + "/AutoBuildDataObject.asset",true);
                AssetDatabase.SaveAssets();

                System.Diagnostics.Process.Start(dataPath);
            }
        }
    }

    private void Load()
    {
        var guicontent = new GUIContent("Load", "Build ���� �ҷ�����");
        if (GUILayout.Button(guicontent, guiStyle))
        {
            string dataPath = EditorUtility.OpenFilePanel("Path", "", "asset");

            if (!string.IsNullOrWhiteSpace(dataPath) && File.Exists(dataPath))
            {
                BuildCommonMethod.SetInfoToEditor(BuildCommonMethod.LoadDataObject(dataPath));
            }
        }


    }

}
#endif
