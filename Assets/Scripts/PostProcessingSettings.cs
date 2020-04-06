using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingSettings : MonoBehaviour
{
    public float BloomIntensity = 1f;
    public float VignetteIntensity = 0.64f;

    Bloom bloomLayer = null;
    Vignette vignetteLayer = null;
    PostProcessVolume ppv;

    void Start()
    {
        //Getting Information
        BloomIntensity = PlayerPrefs.GetFloat("Bloom", 1f);
        VignetteIntensity = PlayerPrefs.GetFloat("Vignette", 0.64f);
        
        //Applying the information
        
        //Instatiating the Post Process Volume Variable
        ppv = GetComponent<PostProcessVolume>();
        
        //Applying intensity values to variables
        ppv.profile.TryGetSettings(out bloomLayer);
        ppv.profile.TryGetSettings(out vignetteLayer);
        bloomLayer.intensity.value = BloomIntensity;
        vignetteLayer.intensity.value = VignetteIntensity;
        
        ppv.profile.AddSettings(bloomLayer);
        ppv.profile.AddSettings(vignetteLayer);

    }
}
