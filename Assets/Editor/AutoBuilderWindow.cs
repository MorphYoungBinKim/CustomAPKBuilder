#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using UnityEngine.Events;

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
    private Vector2 windowSize = new Vector2(500, 500);
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
        propertyRect = new Rect(5, 25, position.width-10, 300);
        buildpropertyRect = new BuildPropertyRect();
        
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
        buildpropertyRect.OnGUI(propertyRect);
        if (GUI.Button(new Rect(5, windowSize.y - 35, position.width - 10, 30), "Build"))
        {

        }
        //editorScript.OnGUI(editorRect);
    }


    #region ShowWindowr
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