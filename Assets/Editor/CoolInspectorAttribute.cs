using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoolInspectorDemo))]
public class CoolInspectorEditor : Editor
{
    float colorValue = 0.5f;

    public override void OnInspectorGUI()
    {
        // 標題
        EditorGUILayout.LabelField("🎮 Cool Inspector Demo", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        // 取得目標腳本
        CoolInspectorDemo demo = (CoolInspectorDemo)target;

        // 可調整速度的滑桿
        demo.speed = EditorGUILayout.Slider("Speed", demo.speed, 0f, 10f);

        // 顏色控制滑桿
        colorValue = EditorGUILayout.Slider("Color Hue", colorValue, 0f, 1f);

        // 顯示一個顏色方塊
        Color previewColor = Color.HSVToRGB(colorValue, 1, 1);
        Rect rect = GUILayoutUtility.GetRect(50, 50);
        EditorGUI.DrawRect(rect, previewColor);

        // 按鈕：讓物件變色+旋轉
        if (GUILayout.Button("✨ Activate Magic"))
        {
            demo.ApplyMagic(previewColor);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("用滑桿控制顏色，用按鈕施展魔法！", MessageType.Info);

        // 更新 Inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(demo);
        }
    }
}
