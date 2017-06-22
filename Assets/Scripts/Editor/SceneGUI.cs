using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomEditor(typeof(GameObject))]
[CanEditMultipleObjects]
public class SceneGUI : EditorWindow {

    //void OnEnable()
    //{
    //    SceneView.onSceneGUIDelegate += OnScene;
    //}

    //void OnDisable()
    //{
    //    SceneView.onSceneGUIDelegate -= OnScene;
    //}

    //void OnScene(SceneView scene)
    //{
    //    Handles.BeginGUI();
    //    GUILayout.Button("Hi");
    //    Handles.EndGUI();
    //}

    [MenuItem("Window/Scene GUI/Enable")]
    public static void Enable()
    {
        SceneView.onSceneGUIDelegate += OnScene;
        Debug.Log("Scene GUI : Enabled");
    }

    [MenuItem("Window/Scene GUI/Disable")]
    public static void Disable()
    {
        SceneView.onSceneGUIDelegate -= OnScene;
        Debug.Log("Scene GUI : Disabled");
    }

    private static void OnScene(SceneView sceneview)
    {
        Handles.BeginGUI();
        if (GUILayout.Button("Press Me"))
            Debug.Log("Got it to work.");

        Handles.EndGUI();
    }
}
