using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(DrawGrid))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawGrid drawGrid = (DrawGrid)target;
        if (GUILayout.Button("Draw grid"))
            drawGrid.Draw();
    }
}
#endif