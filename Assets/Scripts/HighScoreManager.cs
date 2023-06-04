using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{

    private Transform _highScoreTemplate;
    private Transform _highScoreTemplateContainer;
    private List<string> _scoreList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        _highScoreTemplateContainer = transform.Find("ScoreTemplateContainer");
        _highScoreTemplate = _highScoreTemplateContainer.Find("ScoreTemplate");
        _highScoreTemplate.gameObject.SetActive(false);


        LoadScoresFromPlayerPrefs();

        // Display score at least 1 score in list
        if (this._scoreList.Count > 0)
        {
            SortScoresDescending();


            int totalScoreCount = _scoreList.Count;

            // Display all the scores
            for (int i = 0; i < totalScoreCount; i++)
            {
                Transform entryTransform = Instantiate(_highScoreTemplate, _highScoreTemplateContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();

                entryRectTransform.anchoredPosition = new Vector2(0, -25.5f * i);
                entryTransform.gameObject.SetActive(true);


                int rank = i + 1;
                string rankPostFix;

                switch (rank)
                {
                    case 1: rankPostFix = "st"; break;
                    case 2: rankPostFix = "nd"; break;
                    case 3: rankPostFix = "rd"; break;
                    default: rankPostFix = "th"; break;
                }
                entryTransform.Find("PositionText").GetComponent<Text>().text = $"{rank}{rankPostFix}";
                entryTransform.Find("ScoreText").GetComponent<Text>().text = $"{_scoreList[i].Trim()}";
            }
        }
    }



    public void SaveScoresToPlayerPrefs()
    {
        PlayerPrefs.SetString("ScoreList", JsonConvert.SerializeObject(_scoreList));
    }



    private void LoadScoresFromPlayerPrefs()
    {
        this._scoreList = JsonConvert.DeserializeObject<List<string>>(PlayerPrefs.GetString("ScoreList"));
    }



    private void SortScoresDescending()
    {
        this._scoreList.Sort((a, b) => Convert.ToInt32(b).CompareTo(Convert.ToInt32(a)));
    }
}
