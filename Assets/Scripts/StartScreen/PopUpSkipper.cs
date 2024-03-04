using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSkipper : MonoBehaviour
{
    public Toggle toggle;
    public void checkIfToggled()
    {
         if (toggle.isOn)
        {
            PlayerPrefs.SetInt("PopUpSkipper", 1);

        }
    }
}
