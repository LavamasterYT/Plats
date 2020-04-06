using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public void NextLevel(string levelName)
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene(levelName);
    }
}
