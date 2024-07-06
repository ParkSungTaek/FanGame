using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Client;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class TestTool : Editor
{
#if UNITY_EDITOR
    [MenuItem("TestTool/Test1")]
    public static void Test1()
    {


    }

    [MenuItem("TestTool/Test2")]
    public static void Test2()
    {
        System.GC.Collect();
        System.GC.Collect();
        System.GC.Collect();
        System.GC.Collect();
        System.GC.Collect();
        System.GC.Collect();
        System.GC.Collect();
    }

    [MenuItem("TestTool/Test3")]
    public static void Test3()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
#endif
}
