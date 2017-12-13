using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pontuationScript : MonoBehaviour
{

    //texts
    public Text scoreText;
    public Text maxScoreText;

    // Use this for initialization
    void Start()
    {
        if (System.IO.File.Exists("tempFileForSavingScore.txt"))
        {
            string pontuation = System.IO.File.ReadAllText("tempFileForSavingScore.txt");
            scoreText.text = "Score " + pontuation;
            System.IO.File.Delete("tempFileForSavingScore.txt");
        }
        else
        {
            scoreText.text = "Error Loading Score";
        }
        if (System.IO.File.Exists("highScores.txt"))
        {
            string pontuation = System.IO.File.ReadAllText("highScores.txt");
            maxScoreText.text = "High Score " + pontuation;
        }
        else
        {
            maxScoreText.text = "High Score " + "0";
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
