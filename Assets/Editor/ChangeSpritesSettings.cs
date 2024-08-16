using UnityEngine;
using UnityEditor;

public class ChangeSpritesSettings : EditorWindow
{
    [MenuItem("Tools/Change Sprites Settings")]
    public static void ShowWindow()
    {
        GetWindow<ChangeSpritesSettings>("Change Sprites Settings");
    }

    private float pixelsPerUnit = 64f;
    private FilterMode filterMode = FilterMode.Point;
    private int maxTextureSize = 2048;
    private TextureImporterFormat windowsFormat = TextureImporterFormat.Automatic;
    private TextureImporterFormat androidFormat = TextureImporterFormat.Automatic;
    private TextureImporterFormat iosFormat = TextureImporterFormat.Automatic;

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

                TextureImporterPlatformSettings windowsSettings = textureImporter.GetPlatformTextureSettings("Standalone");
                windowsSettings.overridden = false;
                windowsSettings.maxTextureSize = maxTextureSize;
                windowsSettings.format = windowsFormat;
                windowsSettings.compressionQuality = 0;
                textureImporter.SetPlatformTextureSettings(windowsSettings);

                TextureImporterPlatformSettings androidSettings = textureImporter.GetPlatformTextureSettings("Android");
                androidSettings.overridden = false;
                androidSettings.maxTextureSize = maxTextureSize;
                androidSettings.format = androidFormat;
                androidSettings.compressionQuality = 0;
                textureImporter.SetPlatformTextureSettings(androidSettings);

                TextureImporterPlatformSettings iosSettings = textureImporter.GetPlatformTextureSettings("iPhone");
                iosSettings.overridden = false;
                iosSettings.maxTextureSize = maxTextureSize;
                iosSettings.format = iosFormat;
                iosSettings.compressionQuality = 0;
                textureImporter.SetPlatformTextureSettings(iosSettings);

                textureImporter.SaveAndReimport();
            }
        }
        Debug.Log("Settings applied to selected sprites.");
    }
}