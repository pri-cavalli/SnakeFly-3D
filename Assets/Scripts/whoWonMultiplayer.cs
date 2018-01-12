using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class whoWonMultiplayer : MonoBehaviour {
    //texts
    public Text scoreText;
    public Text maxScoreText;

    // Use this for initialization
    void Start()
    {
        if (System.IO.File.Exists("multiplay.txt"))
        {
            string all = System.IO.File.ReadAllText("multiplay.txt");
            string[] substrings = all.Split('-'); // play1 points - play2 points - who die

            scoreText.text = "Scores\nPlayer 1: " + substrings[0] + "\nPlayer 2: " + substrings[1];
            maxScoreText.text = "The player who died: " + substrings[2];

            System.IO.File.Delete("multiplay.txt");
        }
        else
        {
            scoreText.text = "Error Loading Who Won";
            maxScoreText.text = " ";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}