using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScript : MonoBehaviour
{
    public TMP_Text scoreToBeatText;
    public PuckScript puckScript;
    public GameObject pointsChoice;

    private bool gameStarted = false;
    private int scoreToBeat = 3;

    public void FixedUpdate()
    {
        if (gameStarted)
        {
            if (puckScript.redScore >= scoreToBeat)
            {
                GameOver(winner: "The AI");
            }
            else if (puckScript.blueScore >= scoreToBeat)
            {
                GameOver(winner: "You");
            }
        }
    }
    public void PressPlay()
    {
        
    }

    public void IncreaseScore()
    {
        scoreToBeat += 3;
        scoreToBeatText.text = scoreToBeat.ToString();
    }

    public void DecreaseScore()
    {
        if (scoreToBeat > 3)
        {
            scoreToBeat -= 3;
        }
        scoreToBeatText.text = scoreToBeat.ToString();
    }

    public void GameOver(string winner)
    {
        //handle game over logic
        //display winner won, play again?
    }
}
