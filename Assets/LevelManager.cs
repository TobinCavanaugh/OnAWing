using System;
using System.Collections.Generic;
using Player;
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
    public List<GameObject> enableThese;
    public string sceneName;

    [Header("Materials")] public float foliageWindStrength = .06f;
    public float foliageShadowStrength = 0f;

    public float grassWindIntensity = .1f;
    public float grassShadowStrength = .056f;

    [Header("Player")]
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
        
        grass.SetFloat("GustIntensity", grassWindIntensity);
        grass.SetFloat("_ShadowStrength", grassShadowStrength);
    }

    public void DisableObjects()
    {
        enableThese.ForEach(x => x.SetActive(false));
    }
}