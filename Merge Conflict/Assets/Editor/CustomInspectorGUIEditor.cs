/**********************************************************************************************************************
Name:          CustomEditorForAudioManagerInspector
Description:   Changes the GUI for the Unity inspector in the AudioManager when it has unfilled serialized fields.  
Author(s):     Daniel Rittrich
Date:          2024-03-27
Version:       V1.0
TODO:          - /
**********************************************************************************************************************/

using UnityEngine;
using UnityEditor;

public static class CustomEditorForAudioManagerInspector
{
    public static Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    public static void CommonInspectorGUI(SerializedObject serializedObject, GUIStyle emptyFieldStyle)
    {
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (iterator.propertyType == SerializedPropertyType.ObjectReference && iterator.objectReferenceValue == null)
            {
                GUI.color = Color.red;
                EditorGUILayout.PropertyField(iterator, true);
                GUI.color = Color.white;
            }
            else
            {
                EditorGUILayout.PropertyField(iterator, true);
            }
        }
    }
}

public abstract class CommonEditor : Editor
{
    private GUIStyle redBackgroundStyle;

    protected virtual void OnEnable()
    {
        redBackgroundStyle = new GUIStyle(EditorStyles.textField)
        {
            normal = { background = CustomEditorForAudioManagerInspector.MakeTex(2, 2, new Color(1f, 0.5f, 0.5f)) }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        CustomEditorForAudioManagerInspector.CommonInspectorGUI(serializedObject, redBackgroundStyle);
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(AudioManager))]
public class MyClass1Editor : CommonEditor { }

/* [CustomEditor(typeof(WEITERE_KLASSE_2))]
public class MyClass2Editor : CommonEditor { }

[CustomEditor(typeof(WEITERE_KLASSE_3))]
public class MyClass2Editor : CommonEditor { } */
