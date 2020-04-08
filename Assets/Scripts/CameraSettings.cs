using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraSettings : MonoBehaviour
{
    public float BloomIntensity = 1f;
    public float VignetteIntensity = 0.64f;
    public float MouseAxis;

    Bloom bloomLayer = null;
    Vignette vignetteLayer = null;
    PostProcessVolume ppv;

    public GameObject player;
    public Camera camera;

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

        //Getting the camera game object
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        MouseAxis = Input.GetAxisRaw("Mouse ScrollWheel");

        if (Input.GetKeyDown(KeyCode.C))
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
            PlayerPrefs.SetFloat("Size", 5f);
        }

        if (PlayerPrefs.GetInt("FollowPlayer", 0) == 1)
        {
            camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.SetInt("FollowPlayer", (PlayerPrefs.GetInt("FollowPlayer", 0) == 1) ? 0 : 1);
        }

        if (MouseAxis > 0f)
            PlayerPrefs.SetFloat("Size", camera.orthographicSize - 0.5f);
        else if (MouseAxis < 0f)
            PlayerPrefs.SetFloat("Size", camera.orthographicSize + 0.5f);

        camera.orthographicSize = PlayerPrefs.GetFloat("Size", 5f);
    }
}
