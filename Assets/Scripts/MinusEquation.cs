using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinusEquation : MonoBehaviour
{

    public TMP_Text text;
    public TMP_InputField inputField;
    public TMP_Text answer;
    public Button enter;
    public TMP_Text scoreText;
    public int subScore;
    int number1;
    int number2;

    // Start is called before the first frame update
    void Start()
    {
        subScore = PlayerPrefs.GetInt("subPoints");
        scoreText.text = subScore.ToString();
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

        if (input == (number1-number2).ToString()) 
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
        number1 = Random.Range(1,21); //last value is max-1
        number2 = Random.Range(1, number1+1);

        text.text = number1 + "−" + number2;
        
        yield return null;
    }

    IEnumerator Correct()
    {
        enter.interactable = false;
        answer.text = "Rigtig";
        answer.color = Color.green;

        subScore++; //add to score counter

        PlayerPrefs.SetInt("subPoints", subScore); //set new score,
        Debug.Log(PlayerPrefs.GetInt("subPoints")); //show new score

        scoreText.text = subScore.ToString();
        yield return new WaitForSeconds(1f);
        answer.text = "Svar venligst";
        answer.color = Color.black;
        StartCoroutine(NewMath());
        inputField.text = "";
        enter.interactable = true;
    }


}
