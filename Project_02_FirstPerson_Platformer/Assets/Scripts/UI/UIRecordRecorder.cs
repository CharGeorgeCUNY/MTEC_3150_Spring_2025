using TMPro;
using UnityEngine;


namespace WinterSym
{
    public class UIRecordRecorder : MonoBehaviour
    {
        [SerializeField] private string inputText;
        [SerializeField] private Canvas inputCanvas;


        public void GetInput(string input)
        {
            inputText = input;
        }

        public void SendInput()
        {
            //create new high score entry
            UIHighScores.HighScoreChart.AddHighScore(UIHighScores.HighScoreChart.tempFloat, UIHighScores.HighScoreChart.tempString, inputText);

            UIHighScores.HighScoreChart.Sort();

            //show high score chart canvas
            UIHighScores.HighScoreChart.Display();

            //hide this canvas
            gameObject.SetActive(false);
        }


        public void Display()
        {
            inputCanvas.enabled = true;
        }

        public void Hide()
        {
            inputCanvas.enabled = false;
        }
    }
}