using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int Zoom = 5;
    public int Normal = 12;
    
    public float Smoothness = 5f;
    public float InvokeTime = 0.5f;

    public Camera Cam;
    public Animator Menu;
    public Slider BloomSlider;
    public Slider VignetteSlider;
    public Button PlayButton;
    public Button ResetSettings;
    public Button Exit;

    public bool UIElementsDisabled = false;
    public bool ButtonClicked = false;

    private void Start()
    {
        //Keys creation
        if (!(PlayerPrefs.HasKey("Vignette") && PlayerPrefs.HasKey("Bloom")))
        {
            PlayerPrefs.SetFloat("Vignette", 0.64f);
            PlayerPrefs.SetFloat("Bloom", 1f);
        }

        //Set values for sliders
        BloomSlider.value = PlayerPrefs.GetFloat("Bloom", 1f);
        VignetteSlider.value = PlayerPrefs.GetFloat("Vignette", 0.64f);
        PlayerPrefs.SetFloat("Size", 5f);
        PlayerPrefs.SetInt("FollowPlayer", 0);

        //Listeners
        BloomSlider.onValueChanged.AddListener(BloomChanged);
        VignetteSlider.onValueChanged.AddListener(VignetteChanged);
        PlayButton.onClick.AddListener(OnPlayButton);
        ResetSettings.onClick.AddListener(OnResetButton);
        Exit.onClick.AddListener(OnExitButton);
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || ButtonClicked)
        {
            if (UIElementsDisabled == false)
            {
                Menu.SetBool("IsFading", true);
            }
            Invoke("LoadScene", InvokeTime);
            UIElementsDisabled = true;

            
        }
        if (UIElementsDisabled)
        {
            Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, Zoom, Time.deltaTime * Smoothness);
            
        }

        if (Input.GetKey(KeyCode.L))
            SceneManager.LoadScene("Load Level");
    }

    public void LoadScene()
    {
        Debug.Log("Loading 1-1");
        SceneManager.LoadScene("1-1");
    }

    void BloomChanged(float arg0)
    {
        PlayerPrefs.SetFloat("Bloom", arg0);
    }

    public void VignetteChanged(float arg0)
    {
        PlayerPrefs.SetFloat("Vignette", arg0);
    }

    public void OnPlayButton()
    {
        ButtonClicked = true;
    }

    public void OnResetButton()
    {
        PlayerPrefs.SetFloat("Vignette", 0.64f);
        PlayerPrefs.SetFloat("Bloom", 1f);
        BloomSlider.value = PlayerPrefs.GetFloat("Bloom", 1f);
        VignetteSlider.value = PlayerPrefs.GetFloat("Vignette", 0.64f);
    }

    public void OnExitButton()
    {
        Debug.Log("Exiting");
        Application.Quit(0);
    }
}
