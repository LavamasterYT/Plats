using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNumberOfDeaths : MonoBehaviour
{
    private Text DeathCounter;

    // Start is called before the first frame update
    void Start()
    {
        DeathCounter = GetComponent<Text>();
        DeathCounter.text = "Total Deaths: " + PlayerPrefs.GetInt("Deaths", 0);
    }
}
