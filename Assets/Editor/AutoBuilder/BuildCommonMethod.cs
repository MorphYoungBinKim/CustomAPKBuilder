using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10000)]
public static class BuildCommonMethod
{
    private static string savePath = string.Format("{0}/{1}", Application.dataPath, "Editor/AutoBuilder");
    private static string saveFilePath = string.Format("{0}/{1}", savePath, "AutoBuildDataObject.asset");
    public static string SaveFilePath => saveFilePath;
    public static SaveScriptableObject GetSaveDataObject()
    {
        if(!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
        if(!File.Exists(saveFilePath))
        {
            return null;
        }

        try
        {
            SaveScriptableObject saveObj = (SaveScriptableObject)AssetDatabase.LoadAssetAtPath("Assets/Editor/AutoBuilder/AutoBuildDataObject.asset", typeof(SaveScriptableObject));
            return saveObj;
        }
        catch
        {
            return null;
        }
    }

    public static SaveScriptableObject SaveDataObject()
    {
        var saveObject = GetSaveDataObject();
        if (saveObject != null)
        {
            File.Delete(saveFilePath);
        }

        SaveScriptableObject asset = ScriptableObject.CreateInstance<SaveScriptableObject>();
        asset.buildInfoClass = SetBuildInfo(AutoBuilderWindow.Buildinfo);
        AssetDatabase.CreateAsset(asset, "Assets/Editor/AutoBuilder/AutoBuildDataObject.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AutoBuilderWindow.GetInstance.Repaint();

        return saveObject;
    }

    public static SaveScriptableObject LoadDataObject(string path)
    {
        EditorUtility.DisplayProgressBar("Load", "Data", 1 / 3);
   
        File.Copy(path, saveFilePath,true);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayProgressBar("Load", "Data", 2 / 3);
        SaveScriptableObject obj = (SaveScriptableObject)AssetDatabase.LoadAssetAtPath("Assets/Editor/AutoBuilder/AutoBuildDataObject.asset", typeof(SaveScriptableObject));
        
        if (obj != null)
        {
            AutoBuilderWindow.Buildinfo = SetBuildInfo(obj.buildInfoClass);
            AutoBuilderWindow.GetInstance.IsDatainit = false;
            AutoBuilderWindow.GetInstance.Repaint();
        }
        EditorUtility.ClearProgressBar();
        
        return obj;
    }

    public static BuildInfoClass SetBuildInfo(BuildInfoClass value)
    {
        var origin = new BuildInfoClass();
        origin.AppName = value.AppName;
        origin.AppVersion = value.AppVersion;
        origin.BuildPath = value.BuildPath;
        
        UnityEvent tempEvent = new UnityEvent();
        origin.BuildEvent = new BuildEventClass();
        {
            tempEvent = value.BuildEvent.OnAfterBuild.Clone();
            origin.BuildEvent.OnAfterBuild = tempEvent;
            tempEvent = null;
            tempEvent = new UnityEvent();
            tempEvent = value.BuildEvent.OnBeforeBuild.Clone();
            origin.BuildEvent.OnBeforeBuild = tempEvent;
            tempEvent = null;
            tempEvent = new UnityEvent();
            tempEvent = value.BuildEvent.OnBeforeProductBuild.Clone();
            origin.BuildEvent.OnBeforeProductBuild = tempEvent;
            tempEvent = null;
            tempEvent = new UnityEvent();
            tempEvent = value.BuildEvent.OnAfterProductBuild.Clone();
            origin.BuildEvent.OnAfterProductBuild = tempEvent;
            tempEvent = null;
            tempEvent = new UnityEvent();
            tempEvent = value.BuildEvent.OnBeforeStageBuild.Clone();
            origin.BuildEvent.OnBeforeStageBuild = tempEvent;
            tempEvent = null;
            tempEvent = new UnityEvent();
            tempEvent = value.BuildEvent.OnAfterStageBuild.Clone();
            origin.BuildEvent.OnAfterStageBuild = tempEvent;

        }
        //EditorUtility.CopySerialized(origin.BuildEvent.OnBeforeBuild, value.BuildEvent);
        //origin.BuildEvent = value.BuildEvent;
        List<SceneAsset> asset = new List<SceneAsset>();
        foreach (var scene in value.SceneSetting)
        {
            asset.Add(scene);
        }
        origin.SceneSetting = asset;
        origin.VersionCode = value.VersionCode;
        origin.TargetPlatform = value.TargetPlatform;
        origin.TargetType = value.TargetType;
        origin.CompanyName = value.CompanyName;
        origin.ProductName = value.ProductName;
        origin.minSdkVersion = value.minSdkVersion;
        origin.targetSdkVersion = value.targetSdkVersion;
        origin.UseKeyStore = value.UseKeyStore;
        origin.KeyStorePassWord = value.KeyStorePassWord;
        origin.KeyStorePath = value.KeyStorePath;
        origin.UseSchema = value.UseSchema;
        origin.SchemaName = value.SchemaName;

        return origin;
    }

    public static T Clone<T>(this T ev) where T : UnityEventBase
    {
        if(ev == null)
        {
            return null;
        }
        return DeepCopy(ev);
    }

    public static T DeepCopy<T>(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException("Object cannot be null");
        }
        return (T)DoCopy(obj);
    }

    private static object DoCopy(object obj)
    {
        if (obj == null)
        {
            return null;
        }

        // Value type
        var type = obj.GetType();
        if (type.IsValueType || type == typeof(string))
        {
            return obj;
        }

        // Array
        else if (type.IsArray)
        {
            Type elementType = type.GetElementType();
            var array = obj as Array;
            Array copied = Array.CreateInstance(elementType, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                copied.SetValue(DoCopy(array.GetValue(i)), i);
            }
            return Convert.ChangeType(copied, obj.GetType());
        }

        // Unity Object
        else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
        {
            return obj;
        }

        // Class -> Recursion
        else if (type.IsClass)
        {
            var copy = Activator.CreateInstance(obj.GetType());

            var fields = type.GetAllFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                var fieldValue = field.GetValue(obj);
                if (fieldValue != null)
                {
                    field.SetValue(copy, DoCopy(fieldValue));
                }
            }

            return copy;
        }

        // Fallback
        else
        {
            throw new ArgumentException("Unknown type");
        }


    }

    public static List<FieldInfo> GetAllFields(this Type type, BindingFlags flags)
    {
        // Early exit if Object type
        if (type == typeof(System.Object))
        {
            return new List<FieldInfo>();
        }

        // Recursive call
        var fields = type.BaseType.GetAllFields(flags);
        fields.AddRange(type.GetFields(flags | BindingFlags.DeclaredOnly));
        return fields;
    }

    public static void SetInfoToEditor(SaveScriptableObject info)
    {
        PlayerSettings.bundleVersion = info.buildInfoClass.AppVersion;
        PlayerSettings.Android.bundleVersionCode = info.buildInfoClass.VersionCode;
        PlayerSettings.productName = info.buildInfoClass.ProductName;
        PlayerSettings.companyName = info.buildInfoClass.CompanyName;
        PlayerSettings.Android.minSdkVersion = info.buildInfoClass.minSdkVersion;
        PlayerSettings.Android.targetSdkVersion = info.buildInfoClass.targetSdkVersion;
    }
}