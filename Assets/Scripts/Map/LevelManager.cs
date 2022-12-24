using System;
using System.Collections.Generic;
using Player;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<MapElement> mapElements;

    public Material foliageMaterial;
    public Material grassMaterial;
    
    private void Start()
    {
        //var loadedScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(loadedScene.name);
        
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            OnLoadLevel(scene);
        };
        
        OnLoadLevel(SceneManager.GetActiveScene());
    }

    private void OnLoadLevel(Scene scene)
    {
        Debug.Log($"Loaded scene {scene.name}");
        mapElements.ForEach(x =>
        {
            x.DisableObjects();
        });

        var goodMap = mapElements.Find(x => x.sceneName == scene.name);
        goodMap.EnableObjects();
        goodMap.UpdateFeatherCount();
        goodMap.SetMaterialValues(foliageMaterial, grassMaterial);
    }
}

[Serializable]
public class MapElement
{
    private const string LEVEL_STUFF = "Level Stuff";
    
    
    [FoldoutGroup(LEVEL_STUFF)]
    public List<GameObject> enableThese;
    
    [FoldoutGroup(LEVEL_STUFF)]
    public string sceneName;

    [Header("Materials")] 
    [FoldoutGroup(LEVEL_STUFF)]
    public float foliageWindStrength = .06f;
    
    [FoldoutGroup(LEVEL_STUFF)]
    public float foliageShadowStrength = 0f;

    [FoldoutGroup(LEVEL_STUFF)]
    public float grassWindIntensity = .1f;
    
    [FoldoutGroup(LEVEL_STUFF)]
    public float grassShadowStrength = .056f;

    [FoldoutGroup(LEVEL_STUFF)]
    public Gradient foliageGradient;

    [FoldoutGroup(LEVEL_STUFF)] 
    public Color grassTopColor = new Color(141, 192, 79);
    
    [FoldoutGroup(LEVEL_STUFF)]
    public Color grassBottomColor = new(36, 109, 45);


    [FoldoutGroup(LEVEL_STUFF)] 
    public Vector3 sunPivotRotation = new(50, -30, 0); 
    
    [Header("Player")]
    [FoldoutGroup(LEVEL_STUFF)]
    public int featherCount = 0;

    public void EnableObjects()
    {
        enableThese.ForEach(x => x.SetActive(true));
    }

    public void UpdateFeatherCount()
    {
        var p = GameObject.FindObjectOfType<PlayerManager>();
        for (int i = 0; i < featherCount; i++)
        {
            p.AddFeather();
        }
    }
    
    public void SetMaterialValues(Material foliage, Material grass)
    {
        foliage.SetFloat("Wind_Strength", foliageWindStrength);
        foliage.SetFloat("_ShadowStrength", foliageShadowStrength);
        Texture2D tex = new Texture2D(255, 1);

        Color[] colors = new Color[255];
        for (float i = 0; i < 255; i++)
        {
            //colors[(int)i] = Color.black;
            
            colors[(int)i] = foliageGradient.Evaluate(i / 255);
        }
        
        tex.SetPixels(colors);
        tex.Apply();
        
        foliage.SetTexture("_ShadingGradientTexture", tex);
        
        grass.SetFloat("GustIntensity", grassWindIntensity);
        grass.SetFloat("_ShadowStrength", grassShadowStrength);
        grass.SetColor("_ColorTop", grassTopColor);
        grass.SetColor("_ColorBottom", grassBottomColor);
    }

    public void DisableObjects()
    {
        enableThese.ForEach(x => x.SetActive(false));
    }
}