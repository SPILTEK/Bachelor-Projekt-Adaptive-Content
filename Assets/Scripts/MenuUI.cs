using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject buttonsParent; // Reference to the parent GameObject
    public Transform buttonsParentTransform;
    private RectTransform rectTransform;
    public List<Transform> buttonsList; // List to store the children of the current button
    private float yOffset = 25f;
    private float heightOffset = 100f;
    private float rotationAmount = 45f;
    private float animationSpeed = 0.5f;
    public bool isToggled = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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

            RectTransform buttonInfoRect = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetComponent<RectTransform>();
            // Makes the last info box move because the for loop goes out of range
            if (parentIndex == 3)
            {
                RectTransform buttonRect = buttonsList[4].GetComponent<RectTransform>();
                StartCoroutine(moveDownCoroutine(buttonRect, buttonInfoRect));
            }
            for (int i = parentIndex + 1; i < buttonsList.Count; i++)
            {
                RectTransform buttonRect = buttonsList[i].GetComponent<RectTransform>();

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

            RectTransform buttonInfoRect = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetComponent<RectTransform>();
            // Makes the last info box move because the for loop goes out of range
            if (parentIndex == 3)
            {
                RectTransform buttonRect = buttonsList[4].GetComponent<RectTransform>();
                StartCoroutine(moveUpCoroutine(buttonRect, buttonInfoRect));
            }
            for (int i = parentIndex + 1; i < buttonsList.Count; i++)
            {
                RectTransform buttonRect = buttonsList[i].GetComponent<RectTransform>();

                // Modify the position in coroutine
                StartCoroutine(moveUpCoroutine(buttonRect, buttonInfoRect));
            }
        }
        //Passes current button through to coroutine
        Button currentButton = EventSystem.current.currentSelectedGameObject.transform.GetComponent<Button>();
        StartCoroutine(DisableEnableButton(currentButton));
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
        Vector2 startHeightInfo = currentInfo.sizeDelta;
        Vector2 targetHeightInfo = startHeightInfo + new Vector2(0, heightOffset);

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
        Vector2 startHeightInfo = currentInfo.sizeDelta;
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

    private IEnumerator DisableEnableButton(Button currentButton)
    {
        //Disable and enable button after set time
        currentButton.interactable = false;
        yield return new WaitForSeconds(0.5f);
        currentButton.interactable = true;
    }
}
