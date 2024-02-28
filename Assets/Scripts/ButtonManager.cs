using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadAddition()
    {
        SceneManager.LoadScene("Addition");
    }

    public void LoadSubtraction()
    {
        SceneManager.LoadScene("Subtraction");
    }

    public void LoadDivision()
    {
        SceneManager.LoadScene("Division");
    }

    public void LoadMultiplication()
    {
        SceneManager.LoadScene("Multiplication");
    }
}
