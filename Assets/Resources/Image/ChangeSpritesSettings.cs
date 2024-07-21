using UnityEngine;
using UnityEditor;

// Copilot을 사용하여 작성된 코드입니다.

public class ChangeSpritesSettings : EditorWindow
{
    // 상단 Window의 Tools에서 Change Sprites Settings를 추가
    [MenuItem("Tools/Change Sprites Settings")]
    public static void ShowWindow()
    {
        GetWindow<ChangeSpritesSettings>("Change Sprites Settings");
    }

    // 인스펙터 창에 표시될 변수들
    private float pixelsPerUnit = 64f;
    private FilterMode filterMode = FilterMode.Point;
    private int maxTextureSize = 2048;
    private TextureImporterFormat windowsFormat = TextureImporterFormat.Automatic;
    private TextureImporterFormat androidFormat = TextureImporterFormat.Automatic;
    private TextureImporterFormat iosFormat = TextureImporterFormat.Automatic;

    // 선택된 스프라이트들의 설정을 변경하는 함수
    private void OnGUI()
    {
        GUILayout.Label("Change Sprite Settings", EditorStyles.boldLabel);

        pixelsPerUnit = EditorGUILayout.FloatField("Pixels Per Unit", pixelsPerUnit);
        filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", filterMode);
        maxTextureSize = EditorGUILayout.IntField("Max Texture Size", maxTextureSize);

        GUILayout.Label("Override Formats", EditorStyles.boldLabel);
        windowsFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("Windows Format", windowsFormat);
        androidFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("Android Format", androidFormat);
        iosFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("iOS Format", iosFormat);

        if (GUILayout.Button("Apply"))
        {
            ApplySettingsToSelectedSprites();
        }
    }

    private void ApplySettingsToSelectedSprites()
    {
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null && textureImporter.textureType == TextureImporterType.Sprite)
            {
                textureImporter.spritePixelsPerUnit = pixelsPerUnit;
                textureImporter.filterMode = filterMode;

                // Override for Windows
                TextureImporterPlatformSettings windowsSettings = textureImporter.GetPlatformTextureSettings("Standalone");
                windowsSettings.overridden = true;
                windowsSettings.maxTextureSize = maxTextureSize;
                windowsSettings.format = windowsFormat;
                textureImporter.SetPlatformTextureSettings(windowsSettings);

                // Override for Android
                TextureImporterPlatformSettings androidSettings = textureImporter.GetPlatformTextureSettings("Android");
                androidSettings.overridden = true;
                androidSettings.maxTextureSize = maxTextureSize;
                androidSettings.format = androidFormat;
                textureImporter.SetPlatformTextureSettings(androidSettings);

                // Override for iOS
                TextureImporterPlatformSettings iosSettings = textureImporter.GetPlatformTextureSettings("iPhone");
                iosSettings.overridden = true;
                iosSettings.maxTextureSize = maxTextureSize;
                iosSettings.format = iosFormat;
                textureImporter.SetPlatformTextureSettings(iosSettings);

                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
        }

        Debug.Log("Settings applied to selected sprites.");
    }
}