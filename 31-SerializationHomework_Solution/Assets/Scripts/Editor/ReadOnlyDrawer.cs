
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace

{
    // расширение инспектора гооврит Юнити как рисовать все поля с атрибутом [ReadOnly].
    //файл лежит в папке Editor, поэтому не попадает в билд игры.

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer

    {
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // высота поля такая же, как и обычного 
            return EditorGUI.GetPropertyHeight(property, label, true);

        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var previous = GUI.enabled;
            GUI.enabled = false; // поле рисуется серым изменить его нельзя 
            EditorGUI.PropertyField(position, property, label, true); 
            GUI.enabled = previous; // возвращаем как было чтоб не выкл ост поля 


        }



    }



}