using UnityEngine;
using UnityEditor;

namespace Maelstrom
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    /// <summary>
    /// Custom property drawer for ReadOnlyAttribute.
    /// </summary>
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
