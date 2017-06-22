using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum PrimitiveShapes
{
    CUBE = 0,
    SPHERE,
    PLANE,
    CAPSULE,
    CYLINDER
}

[CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(EditorBase))]
public class CustomEditor : Editor {

    EditorBase instance;
    PrimitiveShapes shapeChoose;
    List<string> assetNames;
    int assetNamesSelect = 0;
    bool PrimsOrAssets = false;

    void OnEnable()
    {
        foreach (GameObject obj in instance.Assets.Assets)
        {
            assetNames.Add(obj.name);
        }
    }

    public override void OnInspectorGUI()
    {
        instance = (EditorBase)target;
        EditorGUILayout.LabelField("", "Hi");
        EditorGUILayout.Space();

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        //GUILayout.Space(10);

        EditorGUILayout.LabelField("Total Enemy Types: " + instance.enemyTypes.Count, GUILayout.MaxWidth(140));
        if (GUILayout.Button("Create New Enemy Type"))
        {
            AddNewEnemyType();
        }
        GUILayout.Space(10);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.Space(20);
        PrimsOrAssets = EditorGUILayout.Toggle("Spawn Primitive", PrimsOrAssets);
        if(PrimsOrAssets)
            shapeChoose = (PrimitiveShapes)EditorGUILayout.EnumPopup("Primitive to Spawn", shapeChoose);
        else
            assetNamesSelect = EditorGUILayout.Popup(assetNamesSelect, assetNames.ToArray());
        if (instance.enemyTypes.Count > 0)
        {
            GUIStyle style = new GUIStyle();
            //style.fontSize = 30;
            style.normal.textColor = Color.white;
            style.contentOffset = new Vector2(120, 0);
            EditorGUILayout.LabelField("Created Objects", style);
        }
        EditorGUILayout.BeginVertical();
        for(int i =0; i < instance.enemyTypes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            Color col = new Color(0, 0, 0);
            col = EditorGUILayout.ColorField(col);
            if(GUILayout.Button("Spawn"))
            {
                if (PrimsOrAssets)
                    SpawnEnemyPrim(i, col, shapeChoose);
                else
                    SpawnEnemyAsset(col, assetNamesSelect);

            }
            if (GUILayout.Button("Despawn"))
            {
                DeleteEnemy(i);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
            for (int k = 0; k < instance.enemyTypes[i].transform.childCount; k++)
            {
                EditorGUILayout.BeginHorizontal();
                instance.enemyTypes[i].transform.GetChild(k).name = GUILayout.TextField(instance.enemyTypes[i].transform.GetChild(k).name);
                if (GUILayout.Button("Despawn"))
                {
                    DestroyImmediate(instance.enemyTypes[i].transform.GetChild(k).gameObject);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    void AddNewEnemyType()
    {
        SpawnEnemyGroup(new GameObject());
    }

    void SpawnEnemyGroup(GameObject buff)
    {
        buff.name = "Enemy Group " + (instance.enemyTypes.Count + 1).ToString();
        instance.enemyTypes.Add(buff);
    }

    void SpawnEnemyAsset(Color col, int assetIndex)
    {
        GameObject buff = instance.Assets.Assets[assetIndex];
        Instantiate(buff);
    }

    void SpawnEnemyPrim(int index, Color col, PrimitiveShapes primShape)
    {
        GameObject buff;
        switch (primShape)
        {
            case PrimitiveShapes.CUBE:
                buff = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //buff = Instantiate(buff);
                buff.transform.parent = instance.enemyTypes[index].transform;
                break;
            case PrimitiveShapes.SPHERE:
                buff = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //buff = Instantiate(buff);
                buff.transform.parent = instance.enemyTypes[index].transform;
                break;
            case PrimitiveShapes.PLANE:
                buff = GameObject.CreatePrimitive(PrimitiveType.Plane);
                //buff = Instantiate(buff);
                buff.transform.parent = instance.enemyTypes[index].transform;
                break;
            case PrimitiveShapes.CAPSULE:
                buff = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                //buff = Instantiate(buff);
                buff.transform.parent = instance.enemyTypes[index].transform;
                break;
            case PrimitiveShapes.CYLINDER:
                buff = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                //buff = Instantiate(buff);
                buff.transform.parent = instance.enemyTypes[index].transform;
                break;
            default:
                Debug.LogError("Primitive Load Error, Unidentified Shape");
                break;
        }
    }

    void DeleteEnemy(int index)
    {
        GameObject buff = instance.enemyTypes[index];
        instance.enemyTypes.RemoveAt(index);
        DestroyImmediate(buff);
    }
}
