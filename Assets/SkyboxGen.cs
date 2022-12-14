using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class SkyboxGen : MonoBehaviour
{
    public Camera camera;
    public RenderTexture renderTexture;

    private (Vector3, string)[] Directions = new (Vector3, string)[]
    {
        (Vector3.zero, "FRONT"),
        (new(0, 90, 0), "RIGHT"),
        (new(0, -90, 0), "LEFT"),
        (new(90, 0, 0), "DOWN"),
        (new(-90, 0, 0), "UP"),
        (new(0, 180, 0), "BEHIND"),
        
    };

    [Button]
    public void GenerateCube()
    {
        Directions.ForEach(d =>
        {
            camera.transform.localEulerAngles = d.Item1;
            GenerateSingle(d.Item2);
        });
    }
    
    public void GenerateSingle(string name)
    {
        camera.Render();
        var tex = new Texture2D(renderTexture.width, renderTexture.height, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
        RenderTexture.active = renderTexture;
        var rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        tex.ReadPixels(rect, 0, 0);
        AssetDatabase.CreateAsset(tex, $"Assets/Cubemap/{name}{Time.time}.asset");
    }

    [Button]
    public void UpdateRenderTexture()
    {
        camera.Render();
    }
}
