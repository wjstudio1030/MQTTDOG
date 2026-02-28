using UnityEngine;
using UnityEditor;

public class ReadOnlyAttribute : PropertyAttribute
{
    //最終場景改顏色[ReadOnly(r,g,b), SerializeField]
    public Color color;

    public ReadOnlyAttribute()
    {
        color = new Color(0.9f, 0.9f, 0.9f);
    }

    public ReadOnlyAttribute(float r, float g, float b)
    {
        color = new Color(r, g, b);
    }
}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        /*GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;*/

        /*第一個情境只在遊戲運行時唯讀：bool wasEnabled = GUI.enabled;//?? 記錄原本狀態
        if (Application.isPlaying) // 只在遊戲運行時唯讀
            GUI.enabled = false;

        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = wasEnabled;// ? 保證恢復*/

        /*第二個情境搖桿顯示速度EditorGUI.BeginProperty(position, label, property);

        // 限制範圍 0~14 的滑桿
        property.floatValue = EditorGUI.Slider(position, label, property.floatValue, 0, 14);

        EditorGUI.EndProperty();*/

        /*第三情境讓欄位變高的按鈕
        EditorGUI.PropertyField(position, property, label);
        Rect buttonRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, 18);

        if (GUI.Button(buttonRect, "Print Value"))
        {
            Debug.Log($"{property.displayName} = {property.floatValue}");
        }*/

        /*第四情境改變變數顏色的按鈕
        Color oldColor = GUI.color;

        GUI.color = new Color(1f, 0.9f, 2.5f); // 淡紫色背景
        GUI.enabled = false;

        EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = true;
        GUI.color = oldColor;*/

        var attr = (ReadOnlyAttribute)attribute;
        Color oldColor = GUI.color;
        GUI.color = attr.color;

        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;

        GUI.color = oldColor;

    }

    /*第三情境讓欄位變高的按鈕
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // 讓欄位變高一點以容納按鈕
        return EditorGUIUtility.singleLineHeight * 5.5f;
    }*/

    }
