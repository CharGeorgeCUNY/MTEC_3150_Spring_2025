using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WinterSym
{
    public class UIHighScores : MonoBehaviour
    {
        [Header("Components")]
        public static UIHighScores HighScoreChart;

        public UIHighScores GetScoreChart()
        {
            if (HighScoreChart == null)
            {
                HighScoreChart = GameObject.FindFirstObjectByType<UIHighScores>();
            }

            else if (HighScoreChart != this)
            {
                Destroy(gameObject);
            }

            return HighScoreChart;
        }

        [SerializeField] private Transform entryContainer;
        [SerializeField] private Transform entryTemplate;
        [SerializeField] private Canvas entryCanvas;
        [SerializeField] public Canvas inputField;

        public float tempFloat;
        public string tempString;

        public List<UIHighScoreEntry> highScoreEntries;
        private List<Transform> highScoreEntryTransforms;

        void Awake()
        {
            GetScoreChart();

            Hide();
            inputField.gameObject.GetComponent<UIRecordRecorder>().Hide();

            highScoreEntries = new List<UIHighScoreEntry>() {};

            highScoreEntryTransforms = new List<Transform>();
            foreach (UIHighScoreEntry highScoreEntry in highScoreEntries)
            {
                InstantiateHighScoreEntry(highScoreEntry, entryContainer, highScoreEntryTransforms);
            }

            HighScores highScores = new HighScores { highScoreEntries = highScoreEntries };
            string json = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }

        private class HighScores
        {
            public List<UIHighScoreEntry> highScoreEntries;
        }
        private void InstantiateHighScoreEntry(UIHighScoreEntry entry, Transform container, List<Transform> entries)
        {
            float templateHeight = 30f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * entries.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = entries.Count + 1;
            entryTransform.Find("rank").GetComponent<TextMeshPro>().text = rank.ToString();

            entryTransform.Find("score").GetComponent<TextMeshPro>().text = entry.score.ToString();

            entryTransform.Find("name").GetComponent<TextMeshPro>().text = entry.name;
        }

        public void AddHighScore(float scoreFloat, string score, string name)
        {
            UIHighScoreEntry highScoreEntry = new UIHighScoreEntry { scoreFloat = scoreFloat, score = score, name = name };
            highScoreEntries.Add(highScoreEntry);
        }

        public bool CheckAgainstExistingEntries(float newTime)
        {
            //if we have the maximum amount of entries
            if (highScoreEntries.Count == 6)
            {
                //and the new time is less than the existing sixth entry
                if (newTime < highScoreEntries[5].scoreFloat)
                {
                    return true;
                }

                //if not...
                return false;
            }

            //if we havent reached 6 yet
            else return true;
        }

        public void Display()
        {
            entryCanvas.enabled = true;
        }

        public void Hide()
        {
            entryCanvas.enabled = false;
        }

        public void Sort()
        {
            for (int i = 0; i < highScoreEntries.Count; i++)
            {
                for (int j = i + 1; j < highScoreEntries.Count; j++)
                {
                    if (highScoreEntries[j].scoreFloat > highScoreEntries[i].scoreFloat)
                    {
                        UIHighScoreEntry tmp = highScoreEntries[i];
                        highScoreEntries[i] = highScoreEntries[j];
                        highScoreEntries[j] = tmp;
                    }
                }
            }
        }

        public void UpdateTempTime(float newFloat, string newString)
        {
            tempFloat = newFloat;
            tempString = newString;
        }

        public void TossSixthEntry()
        {
            highScoreEntries.RemoveAt(5);
        }

    [Serializable] public class UIHighScoreEntry
    {
        public float scoreFloat;
        public string score;
        public string name;
    }
    }
}
