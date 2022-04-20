using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class ScreenshotUtility : MonoBehaviour
{

    public List<GameObject> sceneObjects;
    public List<InventoryItemData> dataObjects;

    private Camera camera;
    public string pathFolder;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots()
    {
        StartCoroutine(Screenshot());
    }

    void TakeScreenshot(string fullPath)
        {
            if (GetComponent<Camera>() == null)
            {
                camera = GetComponent<Camera>();
            }

            RenderTexture rt = new RenderTexture(256, 256, 24);
            GetComponent<Camera>().targetTexture = rt;
            Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null;

            if (Application.isEditor)
            {
                DestroyImmediate(rt);
            }
            else
            {
                Destroy(rt);
            }

            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath, bytes);
            AssetDatabase.Refresh();
    }

    private IEnumerator Screenshot()
    {
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            GameObject obj = sceneObjects[i];
            InventoryItemData data = dataObjects[i];

            obj.gameObject.SetActive(true);

            yield return null;

            TakeScreenshot($"{Application.dataPath}/{pathFolder}/{data.id}_Icon.png");

            yield return null;
            obj.gameObject.SetActive(false);

            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{pathFolder}/{data.id}_Icon.png");
            if (s != null)
            {
                data.icon = s;
                EditorUtility.SetDirty(data);
            }

            yield return null;
        }
    }
}
