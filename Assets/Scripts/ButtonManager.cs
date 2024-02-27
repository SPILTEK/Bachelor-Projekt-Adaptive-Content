using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadAddition()
    {
        SceneManager.LoadScene("Addition");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
