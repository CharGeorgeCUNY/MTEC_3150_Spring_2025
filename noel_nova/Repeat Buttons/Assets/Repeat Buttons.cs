using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro; 

public class RepeatingButtonsGame: MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public Button redButton, blueButton, greenButton;
    public TextMeshProUGUI scoreText;
    private List<string> sequence = new List<string>();
    private List<string> playerInput = new List<string>();
    private string[] colors = { "Red", "Blue", "Green" };
    private int score = 0;
    private int currentStep = 0;
    private bool playerTurn = false;

    void Start()
    {
        redButton.onClick.AddListener(() => PlayerPress("Red"));
        blueButton.onClick.AddListener(() => PlayerPress("Blue"));
        greenButton.onClick.AddListener(() => PlayerPress("Green"));
        StartNewRound();
    }

    void StartNewRound()
    {
        playerTurn = false;
        playerInput.Clear();
        sequence.Add(colors[Random.Range(0, colors.Length)]);
        StartCoroutine(DisplaySequence());
    }

    IEnumerator DisplaySequence()
    {
        instructionText.text = "Watch the sequence!";
        yield return new WaitForSeconds(1);

        foreach (string color in sequence)
        {
            instructionText.text = color;
            yield return new WaitForSeconds(1);
            instructionText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
        instructionText.text = "Your turn!";
        playerTurn = true;
        currentStep = 0;
    }

    void PlayerPress(string chosenColor)
    {
        if (!playerTurn) return;
        
        playerInput.Add(chosenColor);
        
        if (playerInput[currentStep] != sequence[currentStep])
        {
            instructionText.text = "Wrong! Try again.";
            sequence.Clear();
            score = 0;
            scoreText.text = "Score: " + score;
            Invoke("StartNewRound", 2);
            return;
        }

        currentStep++;
        
        if (currentStep == sequence.Count)
        {
            score++;
            scoreText.text = "Score: " + score;
            Invoke("StartNewRound", 1);
        }
    }
}