// Oz
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
[CustomEditor(typeof(LevelManager))]
public class LevelEditor : Editor
{
    SerializedObject serializedLM;
    SerializedProperty levels;
    SerializedProperty pixelColorMappings;
    LevelManager lm;

    private Color pixelColor;
    private float prefabSize = 8f;
    //private float prefabSize = 2.4f;

    private bool foldLevels = true;
    private bool foldColors = true;
    private void OnEnable()
    {
        lm = (LevelManager)target;
        serializedLM = new SerializedObject(lm);
        levels = serializedLM.FindProperty("levels");
        pixelColorMappings = serializedLM.FindProperty("pixelColorMappings");


    }
    public override void OnInspectorGUI()
    {

        serializedLM.Update();
        EditorGUILayout.HelpBox("Welcome to LevelEditor v0.1 \n This is a simple level generator. To use this tool: \n"
                            + " 1-) Make an 9x16 Portrait pixel map. Use specific colors for each prefab.\n"
                            + " 2-) Click Add new Color-Map button and select your color and prefab.\n"
                            + " 3-) After color-mapping click Add New Level button to create new level. \n"
                            + " 4-) Select your level map"
                            + " 5-) Click Generate Pipes button to create level-data. Or click Clean pipes button to remove level data. \n"
                            + " 6-) Well Done! You made your game. For preview click preview button and see how is your level look like. \n"
                            + " 7-) You can delete pipes when your job is done. To do this just click Clear Previewed pipes button a few times.",
                            MessageType.Info);


        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Color mappings
        GUILayout.Label("Color Mappings", EditorStyles.boldLabel);
        foldColors = EditorGUILayout.Foldout(foldColors, "Colors");
        if (foldColors)
        {
            GUILayout.BeginHorizontal();
            // Add level 
            if (GUILayout.Button("Add New Color-Map"))
                lm.pixelColorMappings.Add(new PixelToObject());
            if (GUILayout.Button("Remove Last Color-Map"))
                pixelColorMappings.DeleteArrayElementAtIndex(pixelColorMappings.arraySize - 1);
            GUILayout.EndHorizontal();
            for (int i = 0; i < pixelColorMappings.arraySize; i++)
            {
                SerializedProperty colors = pixelColorMappings.GetArrayElementAtIndex(i);
                SerializedProperty pixelColor = colors.FindPropertyRelative("pixelColor");
                SerializedProperty prefab = colors.FindPropertyRelative("prefab");

                EditorGUILayout.PropertyField(pixelColor);
                EditorGUILayout.PropertyField(prefab);
                EditorGUILayout.Space();

            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.Label("Edit Levels", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        // Add level 
        if (GUILayout.Button("Add New Level"))
            lm.levels.Add(new Level());
        if (GUILayout.Button("Remove Last Level"))
            levels.DeleteArrayElementAtIndex(levels.arraySize - 1);
        GUILayout.EndHorizontal();


        SerializedProperty TempLevelObjectsPool = serializedLM.FindProperty("levelTemp");
        EditorGUILayout.PropertyField(TempLevelObjectsPool);


        EditorGUILayout.Space();
        EditorGUILayout.Space();


        if (GUILayout.Button("Clear Previewed Pipes"))
            foreach (Transform child in lm.levelTemp.transform)
                GameObject.DestroyImmediate(child.gameObject);


        foldLevels = EditorGUILayout.Foldout(foldLevels, "Levels");
        if (foldLevels)
        {
            for (int i = 0; i < levels.arraySize; i++)
            {
                SerializedProperty level = levels.GetArrayElementAtIndex(i);
                SerializedProperty mapTexture = level.FindPropertyRelative("mapTexture");
                SerializedProperty pipes = level.FindPropertyRelative("pipes");
                SerializedProperty levelTime = level.FindPropertyRelative("levelTime");
                SerializedProperty sucssesPoint = level.FindPropertyRelative("sucssesPoint");

                EditorGUILayout.LabelField("Level " + (i + 1).ToString() + ": ", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(mapTexture);
                EditorGUILayout.PropertyField(levelTime);
                EditorGUILayout.PropertyField(sucssesPoint);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Generate Pipes!", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    GenerateLevel((Texture2D)mapTexture.objectReferenceValue, pipes);
                }
                if (GUILayout.Button("Clean Pipes", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    pipes.ClearArray();
                }
                if (GUILayout.Button("Preview Pipes", GUILayout.MaxWidth(130), GUILayout.MaxHeight(20)))
                {
                    lm.PreviewObjects(i);
                }
                GUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(pipes);
                // for (int k = 0; k < pipes.arraySize; k++)
                // {
                //     SerializedProperty temp = pipes.GetArrayElementAtIndex(k);
                //     if (temp.GetComponent<Pipe>() != null)
                //     {
                //         Pipe pipe = temp.GetComponent<Pipe>();
                //         EditorGUILayout.LabelField(pipe.Type.ToString() + ": Pipe(" + k.ToString() + ")");

                //         EditorGUILayout.BeginHorizontal();
                //         pipe.solutionNum = EditorGUILayout.IntField("Solution Num: ", pipe.solutionNum);
                //         pipe.Rigidity = EditorGUILayout.Toggle("    Rigidity: ", pipe.Rigidity);
                //         EditorGUILayout.EndHorizontal();
                //         EditorGUILayout.Space();
                //     }
                // }
                EditorGUILayout.EndVertical();

                //EditorGUILayout.PropertyField(pipes);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
        }
        serializedLM.ApplyModifiedProperties();
    }

    private void GenerateLevel(Texture2D mapTexture, SerializedProperty pipes)
    {
        for (int i = 0; i < mapTexture.width; i++)
        {
            for (int j = 0; j < mapTexture.height; j++)
            {
                GenerateObject(i, j, mapTexture, pipes);
            }
        }
    }
    private void GenerateObject(int x, int y, Texture2D mapTexture, SerializedProperty pipes)
    {
        pixelColor = mapTexture.GetPixel(x, y);
        if (pixelColor.a == 0) return;
        foreach (PixelToObject pixelColorMapping in lm.pixelColorMappings)
        {
            if (pixelColorMapping.pixelColor.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(0, (y * prefabSize) + prefabSize / 2, (x * prefabSize) + prefabSize / 2);
                GameObject obj = pixelColorMapping.prefab;
                Pipe.PipeType type = (Pipe.PipeType)System.Enum.Parse(typeof(Pipe.PipeType), obj.name);

                //pipes.InsertArrayElementAtIndex(pipes.arraySize);
                pipes.arraySize++;
                pipes.GetArrayElementAtIndex(pipes.arraySize - 1).FindPropertyRelative("Type").intValue = (int)type;
                pipes.GetArrayElementAtIndex(pipes.arraySize - 1).FindPropertyRelative("pos").vector3Value = pos;
                pipes.GetArrayElementAtIndex(pipes.arraySize - 1).FindPropertyRelative("prefab").objectReferenceValue = obj;
            }
        }
    }
}
