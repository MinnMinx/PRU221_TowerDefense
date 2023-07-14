using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI scoreView;
    public static string SCORE_FORMAT = "";
    // Start is called before the first frame update
    void Start()
    {
        scoreView = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreView != null && !GameManager.HasNoInstance)
            scoreView.text = GameManager.instance.score.ToString(SCORE_FORMAT);
    }
}
