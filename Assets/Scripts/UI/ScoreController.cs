using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI scoreView; 
    // Start is called before the first frame update
    void Start()
    {
        scoreView = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreView.text = "Score: " + GameManager.instance.score;
    }
}
