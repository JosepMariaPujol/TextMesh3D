using UnityEngine;
using Softgraph.TextMesh3D.Runtime;
using UnityEditor;

namespace Softgraph.TextMesh3D.Editor
{
    [CustomEditor(typeof(TextMesh3DContainer))]
    public class TextMesh3DContainerEditor : UnityEditor.Editor
    {
        static GUIStyle s_HelpLabelStyle;

        SerializedProperty m_Text;
        SerializedProperty m_Font;
        SerializedProperty m_Output;
        SerializedProperty m_Extrusion;
        SerializedProperty m_Material;
        SerializedProperty m_Spacing;
        SerializedProperty m_GroupOutput;
        SerializedProperty m_Size;

        const string k_Font = "Font";
        const string k_Output = "Splines/Surfaces/Meshes";
        const string k_Extrusion = "Extrusion";
        const string k_Material = "Material";
        const string k_Spacing = "Adding Spacing";
        const string k_GroupOutput = "Group Output";
        const string k_Size = "Font Size";

        bool showInfo;

        public void OnEnable()
        {
            m_Text = serializedObject.FindProperty("m_Text");
            m_Font = serializedObject.FindProperty("m_Font");
            m_Output = serializedObject.FindProperty("m_Output");
            m_Extrusion = serializedObject.FindProperty("m_Extrusion");
            m_Material = serializedObject.FindProperty("m_Material");
            m_Spacing = serializedObject.FindProperty("m_Spacing");
            m_GroupOutput = serializedObject.FindProperty("m_GroupOutput");
            m_Size = serializedObject.FindProperty("m_Size");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Text, new GUIContent());
            EditorGUILayout.PropertyField(m_Font, new GUIContent(k_Font));
            EditorGUILayout.PropertyField(m_Output, new GUIContent(k_Output));
            EditorGUILayout.PropertyField(m_Size, new GUIContent(k_Size));

            if (m_Output.enumValueIndex != 0)
            {
                EditorGUILayout.PropertyField(m_Material, new GUIContent(k_Material));
            }
            if (m_Output.enumValueIndex == 2)
            {
                EditorGUILayout.PropertyField(m_Extrusion, new GUIContent(k_Extrusion));
            }

            showInfo = EditorGUILayout.Foldout(showInfo , "Advanced");

            if(showInfo)
            {
                EditorGUILayout.PropertyField(m_Spacing, new GUIContent(k_Spacing));
                EditorGUILayout.PropertyField(m_GroupOutput, new GUIContent(k_GroupOutput));
            }

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Apply"))
            {
                ((TextMesh3DContainer) target).Function();
            }
        }
    }
}
