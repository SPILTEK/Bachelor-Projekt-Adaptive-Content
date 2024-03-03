using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    public GameObject buttonsParent; // Reference to the parent GameObject
    public Transform buttonsParentTransform;
    private RectTransform rectTransform;
    public List<Transform> buttonsList; // List to store the children of the current button
    public List<GameObject> buttonsInfoList = new List<GameObject>(); // List to store the children of the current buttons info
    public float yOffset = 100f;
    public float heightOffset = 100f;
    public float rotationAmount = 45f;
    public float animationSpeed = 0.5f;
    public bool isToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Find all info boxes and add them to a list which is reversed to get the right order
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("Info");
        foreach (GameObject obj in foundObjects)
        {
            buttonsInfoList.Add(obj);
        }
        buttonsInfoList.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle()
    {
        // Start toggle button rotation coroutine
        StartCoroutine(toggleCoroutine());
        // Define the RectTransform
        RectTransform toggleRectTransform = EventSystem.current.currentSelectedGameObject.transform.GetComponent<RectTransform>();
        if (isToggled == false) //Makes all the buttons below the one pressed go down by 100 pixels
        {
            int parentIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
            buttonsParent = GameObject.Find("Buttons");
            // Clear the list to ensure it's empty
            buttonsList.Clear();

            // Iterate through the immediate children of the parentTransform
            foreach (Transform child in buttonsParentTransform)
            {
                // Add the child to the list
                buttonsList.Add(child);
            }
            for (int i = parentIndex + 1; i < buttonsList.Count; i++)
            {
                RectTransform buttonRect = buttonsList[i].GetComponent<RectTransform>();
                RectTransform buttonInfoRect = buttonsInfoList[i].GetComponent<RectTransform>();

                // Modify the position in coroutine
                StartCoroutine(moveDownCoroutine(buttonRect, buttonInfoRect));
            }
        }
        else // makes all the buttons below the one pressed collapse back into place.
        {
            int parentIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
            GameObject buttonsParent = GameObject.Find("Buttons");
            // Clear the list to ensure it's empty
            buttonsList.Clear();

            // Iterate through the immediate children of the parentTransform
            foreach (Transform child in buttonsParentTransform)
            {
                // Add the child to the list
                buttonsList.Add(child);
            }
            for (int i = parentIndex + 1; i < buttonsList.Count; i++)
            {
                RectTransform buttonRect = buttonsList[i].GetComponent<RectTransform>();
                RectTransform buttonInfoRect = buttonsInfoList[i].GetComponent<RectTransform>();

                // Modify the position in coroutine
                StartCoroutine(moveUpCoroutine(buttonRect, buttonInfoRect));
            }
        }
        isToggled = !isToggled;
    }

    private IEnumerator toggleCoroutine()
    {
        if (isToggled == false)
        {
            Quaternion startRotation = rectTransform.localRotation;
            Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, rotationAmount);

            float timeElapsed = 0f;

            while (timeElapsed < animationSpeed)
            {
                timeElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(timeElapsed / animationSpeed);
                rectTransform.localRotation = Quaternion.Lerp(startRotation, targetQuaternion, t);
                yield return null;
            }

            rectTransform.localRotation = targetQuaternion; // Ensure we reach the exact target rotation
        }
        else
        {
            Quaternion startRotation = rectTransform.localRotation;
            Quaternion targetQuaternion = Quaternion.Euler(0f, 0f, 0);

            float timeElapsed = 0f;

            while (timeElapsed < animationSpeed)
            {
                timeElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(timeElapsed / animationSpeed);
                rectTransform.localRotation = Quaternion.Lerp(startRotation, targetQuaternion, t);
                yield return null;
            }

            rectTransform.localRotation = targetQuaternion; // Ensure we reach the exact target rotation
        }
    }

    //Handles moving the buttons down with relation to time
    private IEnumerator moveDownCoroutine(RectTransform currentButton, RectTransform currentInfo)
    {
        Vector2 startPositionButton = currentButton.anchoredPosition;
        Vector2 targetPositionButton = startPositionButton + new Vector2(0, -yOffset);
        Vector2 startHeightInfo = currentButton.sizeDelta;
        Vector2 targetHeightInfo = startHeightInfo + new Vector2(0, -heightOffset);

        float timeElapsed = 0f;

        while (timeElapsed < animationSpeed)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / animationSpeed);

            currentButton.anchoredPosition = Vector2.Lerp(startPositionButton, targetPositionButton, t);
            currentInfo.sizeDelta = Vector2.Lerp(startHeightInfo, targetHeightInfo, t);

            yield return null;
        }

        currentButton.anchoredPosition = targetPositionButton; // Ensure we reach the exact target rotation
        currentInfo.sizeDelta = targetHeightInfo; // Ensure we reach the exact target height
    }

    //Handles moving the buttons up with relation to time
    private IEnumerator moveUpCoroutine(RectTransform currentButton, RectTransform currentInfo)
    {
        Vector2 startPositionButton = currentButton.anchoredPosition;
        Vector2 targetPositionButton = startPositionButton + new Vector2(0, yOffset);
        float startHeightInfo = currentButton.rect.height;
        float targetHeightInfo = startHeightInfo - heightOffset;

        float timeElapsed = 0f;

        while (timeElapsed < animationSpeed)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / animationSpeed);

            currentButton.anchoredPosition = Vector2.Lerp(startPositionButton, targetPositionButton, t);
            yield return null;
        }

        currentButton.anchoredPosition = targetPositionButton; // Ensure we reach the exact target rotation
    }

}
