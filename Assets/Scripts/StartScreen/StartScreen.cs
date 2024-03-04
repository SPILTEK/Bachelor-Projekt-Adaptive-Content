using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{

    public RawImage icon;
    public RawImage title;
    public GameObject loadScreen;
    public AudioSource source;
    public AudioClip clip;

    IEnumerator Start()
    {
        icon.canvasRenderer.SetAlpha(0.0f);
        title.canvasRenderer.SetAlpha(0.0f);
        yield return new WaitForSeconds(2.0f);
        FadeIn();
        yield return new WaitForSeconds(1.0f);
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(3.0f);
        FadeOut();
    }


    void FadeIn()
    {
        icon.CrossFadeAlpha(1.0f, 1.0f, false);
        title.CrossFadeAlpha(1.0f, 1.0f, false);
    }

    void FadeOut()
    {
        if(PlayerPrefs.GetInt("PopUpSkipper") == 1)
        {
            SceneManager.LoadScene("Menu");
        }
       else
        {
            loadScreen.SetActive(false);
        }
    }

}
