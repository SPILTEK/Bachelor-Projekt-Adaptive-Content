using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Samlet points: " + (PlayerPrefs.GetInt("addPoints") + PlayerPrefs.GetInt("subPoints") + PlayerPrefs.GetInt("mulPoints") + PlayerPrefs.GetInt("divPoints")).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
