using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Button PlayButton;
    public InputField Level;

    void Start()
    {
        PlayButton.onClick.AddListener(TryLoadLevel);
    }

    void TryLoadLevel()
    {
        try
        {
            SceneManager.LoadScene(Level.text);
        }
        catch (Exception e)
        {
            Debug.Log("Scene does not exist!");
        }
    }
}
