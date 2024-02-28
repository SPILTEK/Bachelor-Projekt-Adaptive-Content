using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DivisionEquation : MonoBehaviour
{

    public TMP_Text text;
    public TMP_InputField inputField;
    public TMP_Text answer;
    public Button enter;
    int number1;
    int number2;
    int number3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NewMath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnswerCheck() //checks if answer is right when clicking button
    {
        string input = inputField.text;
        input = input.Trim();

        if (input == (number3/number2).ToString()) 
        {
            StartCoroutine(Correct());
        }
        else
        {
            answer.text = "Forkert";
            answer.color = Color.red;

        }

    }

    IEnumerator NewMath()
    {
        number1 = Random.Range(1,5)*2; //last value is max-1
        number2 = Random.Range(1,5)*2;
        number3 = number1*number2;

        text.text = number3 + "�" + number2;
        
        yield return null;
    }

    IEnumerator Correct()
    {
        enter.interactable = false;
        answer.text = "Rigtig";
        answer.color = Color.green;
        yield return new WaitForSeconds(1f);
        answer.text = "Svar venligst";
        answer.color = Color.black;
        StartCoroutine(NewMath());
        enter.interactable = true;
    }


}