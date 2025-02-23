using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MainMenu))]
public class MainMenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MainMenu mainMenu = (MainMenu)target;

        // Display the original inspector fields
        DrawDefaultInspector();

        // Display current slider values
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Current Slider Values", EditorStyles.boldLabel);
        EditorGUILayout.FloatField("Music Volume", mainMenu.musicSlider.value);
        EditorGUILayout.FloatField("SFX Volume", mainMenu.sfxSlider.value);
    }
}
