using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateUIElement : MonoBehaviour
{
    public float targetRotation = 45f; // specify the target rotation amount here
    public float rotationSpeed = 0.5f; // specify the rotation speed here

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        Quaternion startRotation = rectTransform.localRotation;
        Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, targetRotation);

        float timeElapsed = 0f;

        while (timeElapsed < rotationSpeed)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / rotationSpeed);
            rectTransform.localRotation = Quaternion.Lerp(startRotation, targetQuaternion, t);
            yield return null;
        }

        rectTransform.localRotation = targetQuaternion; // Ensure we reach the exact target rotation
    }
}